using Microsoft.AspNetCore.Components.Forms;
using MonsterHunterModManager.BlazorApp.Data;

namespace MonsterHunterModManager.BlazorApp.Services;

public interface IPhysicalFileService
{
    List<string> GetFileNames(GameSettings settings, Mod mod);
    string GetFileSize(GameSettings settings, Mod mod);
    int GetFileNumber(GameSettings settings, Mod mod);
    Task UploadFile(GameSettings settings, IBrowserFile file);
    void DeleteModFile(GameSettings settings, Mod mod);
    bool IsModeEnabled(GameSettings settings, Mod mod);
    void EnableMod(GameSettings settings, Mod mod);
    void DisableMod(GameSettings settings, Mod mod);
}