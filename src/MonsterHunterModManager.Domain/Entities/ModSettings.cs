using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Enums;
using Newtonsoft.Json;

namespace MonsterHunterModManager.Domain.Entities
{
    public class ModSettings : BaseEntity
    {
        [JsonIgnore] public bool ShowDetails { get; set; }

        public Guid Id { get; set; }
        public Games Game { get; set; }
        public string ZipFileName { get; set; }
        public bool Enabled { get; set; }
        public int Position { get; set; }
        public List<ModFileSettings> ModFilesSettings { get; set; } = new();
    }

    public class ModFileSettings
    {
        public bool EnabledFile { get; set; } = true;
        public string OriginalName { get; set; }
        public string EnabledFileName { get; set; }
    }
}