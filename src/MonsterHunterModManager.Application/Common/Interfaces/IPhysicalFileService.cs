using MonsterHunterModManager.Domain.Entities;

namespace MonsterHunterModManager.Application.Common.Interfaces;

public interface IPhysicalFileService
{
    List<string> GetFileNames(GameSettings settings, ModSettings modSettings);
    string GetFileSize(GameSettings settings, ModSettings modSettings);
    int GetFileNumber(GameSettings settings, ModSettings modSettings);
    void UploadFile(GameSettings settings, string file);
    void DeleteModFile(GameSettings settings, ModSettings modSettings);
    bool IsModeEnabled(GameSettings settings, ModSettings modSettings);
    void EnableMod(GameSettings settings, ModSettings modSettings);
    void DisableMod(GameSettings settings, ModSettings modSettings);
}