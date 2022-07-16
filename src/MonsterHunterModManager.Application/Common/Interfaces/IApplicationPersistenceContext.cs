using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Common.Interfaces;

public interface IApplicationPersistenceContext
{
    void Save(AppSettings appSettings);
    void Save(GameSettings gameSettings);
    void Save(ModSettings modSettings);

    AppSettings GetAppSettings();
    GameSettings GetGameSettings(Games game);
    List<ModSettings> GetModsSettings(Games game);
    
    void RemoveModSettings(ModSettings mod);
}