using MonsterHunterModManager.BlazorApp.Data;

namespace MonsterHunterModManager.BlazorApp.Services;

public interface ISettingsService
{
    void SaveGameSettings(GameSettings settings);
    GameSettings GetGameSettings(Games game);
}