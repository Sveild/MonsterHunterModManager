using System.IO.Compression;
using Microsoft.AspNetCore.Components.Forms;
using MonsterHunterModManager.BlazorApp.Data;
using MonsterHunterModManager.BlazorApp.Extensions;

namespace MonsterHunterModManager.BlazorApp.Services;

public class PhysicalFileService : IPhysicalFileService
{
    public List<string> GetFileNames(GameSettings settings, Mod mod)
    {
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{mod.FileName}");
        return archive.Entries.Select(e => e.Name).ToList();
    }
        
    public string GetFileSize(GameSettings settings, Mod mod)
    {
        return new FileInfo($"{settings.ModsDirectory}\\{mod.FileName}").Length.GetBytesReadable();
    }

    public int GetFileNumber(GameSettings settings, Mod mod)
    {
        return GetFileNames(settings, mod).Count;
    }
    
    public async Task UploadFile(GameSettings settings, IBrowserFile file)
    {
        var directory = $"{settings.ModsDirectory}";

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        await using var fs = new FileStream($"{directory}\\{file.Name}", FileMode.Create);
        await file.OpenReadStream(1073741824).CopyToAsync(fs);
    }

    public void DeleteModFile(GameSettings settings, Mod mod)
    {
        var file = $"{settings.ModsDirectory}\\{mod.FileName}";
        
        if (File.Exists(file))
            File.Delete(file);
    }

    public bool IsModeEnabled(GameSettings settings, Mod mod)
    {
        var count = 0;
        var files = GetFileNames(settings, mod);

        foreach (var file in files)
            if (File.Exists($"{settings.GameDirectory}\\{file}"))
                count += 1;

        return count == files.Count;
    }

    public void EnableMod(GameSettings settings, Mod mod)
    {
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{mod.FileName}");
        archive.ExtractToDirectory(settings.GameDirectory);
    }

    public void DisableMod(GameSettings settings, Mod mod)
    {
        var files = GetFileNames(settings, mod);

        foreach (var file in files)
        {
            var fileName = $"{settings.GameDirectory}\\{file}";
            
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}