using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Enums;
using MonsterHunterModManager.Domain.ValueObjects;
using MonsterHunterModManager.Infrastructure.Common;
using Newtonsoft.Json;

namespace MonsterHunterModManager.Infrastructure.Persistence;

public class ApplicationPersistenceContext : IApplicationPersistenceContext
{
    private readonly IMediator _mediator;

    public ApplicationPersistenceContext(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #region Saving

    private async Task Save<TSettings>(string subDirectoryPath, string fileName, TSettings settings, bool dispatchEvents = true) where TSettings : BaseEntity
    {
        string path = DirectoryPath.SettingsDirectory;
        
        if (!string.IsNullOrEmpty(subDirectoryPath))
            path += $"\\{subDirectoryPath}";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        File.WriteAllText($"{path}\\{fileName}.json", JsonConvert.SerializeObject(settings));
        
        if (dispatchEvents)
            await _mediator.DispatchDomainEvent(settings);
    }
    
    public async Task Save(AppSettings appSettings)
    {
        await Save(string.Empty, "settings", appSettings);
    }

    public async Task Save(GameSettings gameSettings)
    {
        await Save(gameSettings.Game.ToString(), "settings", gameSettings);
    }

    public async Task Save(ModSettings modSettings)
    {
        await Save(
            modSettings.Game.ToString(),
            modSettings.Id.ToString(),
            modSettings
        );
    }

    public async Task SaveMultiple(List<ModSettings> modsSettings)
    {
        foreach (var modSettings in modsSettings)
        {
            await Save(
                modSettings.Game.ToString(),
                modSettings.Id.ToString(),
                modSettings,
                false
            );
        }
    }

    #endregion

    #region Getting

    private T Get<T>(string subDirectoryPath, string fileName) where T : new()
    {
        string path = DirectoryPath.SettingsDirectory;
        
        if (!string.IsNullOrEmpty(subDirectoryPath))
            path += $"\\{subDirectoryPath}";

        path = $"{path}\\{fileName}.json";
        
        if (!File.Exists(path))
            return new T();
        
        var fileContent = File.ReadAllText(path);
        var settings = JsonConvert.DeserializeObject<T>(fileContent);

        return settings ?? new T();
    }
    
    public AppSettings GetAppSettings()
    {
        return Get<AppSettings>(string.Empty, "settings");
    }

    public GameSettings GetGameSettings(Games game)
    {
        return Get<GameSettings>(game.ToString(), "settings");
    }

    public List<ModSettings> GetModsSettings(Games game)
    {
        var modsSettings = new List<ModSettings>();
        var directoryPath = $"{DirectoryPath.SettingsDirectory}\\{game.ToString()}";

        if (!Directory.Exists(directoryPath))
            return new List<ModSettings>();
            
        var files = Directory.GetFiles(directoryPath);

        foreach (var file in files)
        {
            if (Path.GetFileNameWithoutExtension(file) == "settings")
                continue;
            
            modsSettings.Add(Get<ModSettings>(game.ToString(), Path.GetFileNameWithoutExtension(file)));
        }

        return modsSettings;
    }
    
    #endregion

    #region MyRegion

    public void RemoveModSettings(ModSettings mod)
    {
        var path = $"{DirectoryPath.SettingsDirectory}\\{mod.Game.ToString()}\\{mod.Id.ToString()}.json";
        
        if (File.Exists(path))
            File.Delete(path);
    }

    #endregion
}