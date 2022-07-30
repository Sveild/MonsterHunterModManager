using MonsterHunterModManager.Domain.Common;

namespace MonsterHunterModManager.Domain.ValueObjects;

public class DirectoryPath : ValueObject
{
    public static readonly DirectoryPath BaseApplicationDirectory = new($"{AppDomain.CurrentDomain.BaseDirectory}");
    public static readonly DirectoryPath SettingsDirectory = new($"{AppDomain.CurrentDomain.BaseDirectory}settings");
    public static readonly DirectoryPath ModsDirectory = new ($"{AppDomain.CurrentDomain.BaseDirectory}mods");
    
    public string Path { get; set; } = string.Empty;
    
    static DirectoryPath() { }
    
    private DirectoryPath() { }

    private DirectoryPath(string path)
    {
        Path = path;
    }

    public static implicit operator string(DirectoryPath directoryPath)
    {
        return directoryPath.ToString();
    }
    
    public static explicit operator DirectoryPath(string path)
    {
        return new DirectoryPath(path);
    }
    
    public override string ToString()
    {
        return Path;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Path;
    }
}