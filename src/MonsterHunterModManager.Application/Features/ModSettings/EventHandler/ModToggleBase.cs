using System.Text.RegularExpressions;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.EventHandler;

public abstract class ModToggleBase
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;
    private readonly string PakFileNameFormat = @"re_chunk_000.pak.patch_{0}.pak";
    private readonly string PakPattern = @"(.*(\\|\/)){0,1}re_chunk_000.pak.patch_\d{3}.pak";

    protected ModToggleBase(IApplicationPersistenceContext applicationPersistenceContext, IPhysicalFileService physicalFileService)
    {
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }

    protected void DisableAllMods(Games game)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(game);
        var modsSettings = _applicationPersistenceContext.GetModsSettings(game);
        
        foreach (var modSettings in modsSettings)
        {
            if (_physicalFileService.IsModeEnabled(gameSettings, modSettings))
                _physicalFileService.DisableMod(gameSettings, modSettings);
        }
    }

    protected void EnableAllMods(Games game)
    {
        var modNbr = 0;
        var gameSettings = _applicationPersistenceContext.GetGameSettings(game);
        var modsSettings = _applicationPersistenceContext.GetModsSettings(game)
            .Where(ms => ms.Enabled).OrderBy(ms => ms.Position).ToList();

        foreach (var modSettings in modsSettings)
        {
            if (IsPakMod(modSettings))
            {
                foreach (var file in modSettings.ModFilesSettings)
                {
                    var match = Regex.Match(file.OriginalName, PakPattern);
                    
                    if (!match.Success)
                        continue;

                    file.EnabledFileName = $"{match.Groups[1].Value}{string.Format(PakFileNameFormat, modNbr.ToString("d3"))}";
                    modNbr += 1;
                }
            }

            _physicalFileService.EnableMod(gameSettings, modSettings);
        }

        _applicationPersistenceContext.SaveMultiple(modsSettings);
    }

    private bool IsPakMod(Domain.Entities.ModSettings modSettings)
    {
        foreach (var file in modSettings.ModFilesSettings)
            if (Regex.IsMatch(file.OriginalName, PakPattern)) 
                return true;

        return false;
    }
}