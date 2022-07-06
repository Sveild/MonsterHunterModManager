using MonsterHunterModManager.BlazorApp.Data;
using Newtonsoft.Json;

namespace MonsterHunterModManager.BlazorApp.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IPhysicalFileService _physicalFileService;

        public SettingsService(IPhysicalFileService physicalFileService)
        {
            _physicalFileService = physicalFileService;
        }
        
        public void SaveGameSettings(GameSettings settings)
        {
            if (!Directory.Exists(Constants.SettingsPath))
                Directory.CreateDirectory(Constants.SettingsPath);
            
            File.WriteAllText($"{Constants.SettingsPath}\\{settings.Game}.json", JsonConvert.SerializeObject(settings));
        }
        
        public GameSettings GetGameSettings(Games game)
        {
            var settingsFilePath = $"{Constants.SettingsPath}\\{game}.json";
            
            if (!File.Exists(settingsFilePath))
                CreateNewGameSettings(game);
            
            var fileContent = File.ReadAllText(settingsFilePath);
            var settings = JsonConvert.DeserializeObject<GameSettings>(fileContent);

            CheckMods(settings);

            return settings;
        }

        private void CheckMods(GameSettings settings)
        {
            var directory = $"{settings.ModsDirectory}";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // var toRemove = settings.Mods.Where(mod => !File.Exists($"{directory}\\{mod.FileName}")).ToList();
            //
            // foreach (var file in Directory.GetFiles(directory))
            // {
            //     var fileName = Path.GetFileName(file);
            //     if (!settings.Mods.Any(m => m.FileName == fileName))
            //     {
            //         settings.Mods.Add(new Mod
            //         {
            //             ShowDetails = false,
            //             FileName = fileName,
            //             Enabled = false,
            //         });
            //     }
            // }
            //
            // settings.Mods.RemoveAll(m => toRemove.Contains(m));

            foreach (var mod in settings.Mods)
                if (_physicalFileService.IsModeEnabled(settings, mod))
                    mod.Enabled = true;

            SaveGameSettings(settings);
        }

        private void CreateNewGameSettings(Games game)
        {
            var settings = new GameSettings
            {
                Game = game,
                ModsDirectory = $"{Constants.ModsDirectory}\\{game}",
                Mods = new List<Mod>()
            };
            
            SaveGameSettings(settings);
        }
    }
}