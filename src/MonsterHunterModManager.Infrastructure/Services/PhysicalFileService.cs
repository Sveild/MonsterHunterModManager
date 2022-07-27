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
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{modSettings.ZipFileName}");
        return archive.Entries.Select(e => e.FullName).ToList();
    }
        
    public string GetFileSize(GameSettings settings, ModSettings modSettings)
    {
        return new FileInfo($"{settings.ModsDirectory}\\{modSettings.ZipFileName}").Length.GetBytesReadable();
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
        var file = $"{settings.ModsDirectory}\\{modSettings.ZipFileName}";
        
        if (File.Exists(file))
            File.Delete(file);
    }

    public bool IsModeEnabled(GameSettings settings, ModSettings modSettings)
    {
        var count = 0;
        var enabledCount = modSettings.ModFilesSettings.Count(mfs => mfs.EnabledFile);

        foreach (var modFileSettings in modSettings.ModFilesSettings)
        {
            var file = string.IsNullOrEmpty(modFileSettings.EnabledFileName)
                ? modFileSettings.OriginalName
                : modFileSettings.EnabledFileName;
            
            if (File.Exists($"{settings.GameDirectory}\\{file}"))
                count += 1;
        }
        
        return count == enabledCount;
    }

    public void EnableMod(GameSettings settings, ModSettings modSettings)
    {
        using var archive = ZipFile.OpenRead($"{settings.ModsDirectory}\\{modSettings.ZipFileName}");

        foreach (var entry in archive.Entries)
        {
            var modFileSettings = modSettings.ModFilesSettings.Single(mfs => mfs.OriginalName == entry.FullName);

            if (!modFileSettings.EnabledFile)
                continue;
            
            var extractedFileName = string.IsNullOrEmpty(modFileSettings.EnabledFileName)
                ? modFileSettings.OriginalName
                : modFileSettings.EnabledFileName;
            
            entry.ExtractToFile($"{settings.GameDirectory}\\{extractedFileName}");
        }
    }

    public void DisableMod(GameSettings settings, ModSettings modSettings)
    {
        foreach (var modFileSettings in modSettings.ModFilesSettings)
        {
            var extractedFileName = string.IsNullOrEmpty(modFileSettings.EnabledFileName)
                ? modFileSettings.OriginalName
                : modFileSettings.EnabledFileName;
            var fileName = $"{settings.GameDirectory}\\{extractedFileName}";
            
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
    }
}