using System.IO.Compression;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Extensions;

namespace MonsterHunterModManager.Infrastructure.Services;

public class PhysicalFileService : IPhysicalFileService
{
    private const string PakFileNameRegexPattern = @"re_chunk_000.pak.patch_(\d{3}).pak"; 
        
    public List<string> GetFileNames(GameSettings settings, ModSettings modSettings)
    {
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{modSettings.FileName}");
        return archive.Entries.Select(e => e.Name).ToList();
    }
        
    public string GetFileSize(GameSettings settings, ModSettings modSettings)
    {
        return new FileInfo($"{settings.ModsDirectory}\\{modSettings.FileName}").Length.GetBytesReadable();
    }

    public int GetFileNumber(GameSettings settings, ModSettings modSettings)
    {
        return GetFileNames(settings, modSettings).Count;
    }
    
    public void UploadFile(GameSettings settings, string file)
    {
        var directory = $"{settings.ModsDirectory}";

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        
        var fileName = Path.GetFileName(file);
        File.Copy(file, $"{directory}\\{fileName}");
    }

    public void DeleteModFile(GameSettings settings, ModSettings modSettings)
    {
        var file = $"{settings.ModsDirectory}\\{modSettings.FileName}";
        
        if (File.Exists(file))
            File.Delete(file);
    }

    public bool IsModeEnabled(GameSettings settings, ModSettings modSettings)
    {
        var count = 0;
        var files = GetFileNames(settings, modSettings);

        foreach (var file in files)
            if (File.Exists($"{settings.GameDirectory}\\{file}"))
                count += 1;

        return count == files.Count;
    }

    public void EnableMod(GameSettings settings, ModSettings modSettings)
    {
        // var fileNames = GetFileNames(settings, mod);
        
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{modSettings.FileName}");
        archive.ExtractToDirectory(settings.GameDirectory);
    }

    public void DisableMod(GameSettings settings, ModSettings modSettings)
    {
        var files = GetFileNames(settings, modSettings);

        foreach (var file in files)
        {
            var fileName = $"{settings.GameDirectory}\\{file}";
            
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}