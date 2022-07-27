using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Common.Interfaces;

public interface IApplicationPersistenceContext
{
    Task Save(AppSettings appSettings);
    Task Save(GameSettings gameSettings);
    Task Save(ModSettings modSettings);

    Task SaveMultiple(List<ModSettings> modsSettings);

    AppSettings GetAppSettings();
    GameSettings GetGameSettings(Games game);
    List<ModSettings> GetModsSettings(Games game);
    
    void RemoveModSettings(ModSettings mod);
}