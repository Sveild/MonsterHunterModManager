using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Enums;
using Newtonsoft.Json;

namespace MonsterHunterModManager.Domain.Entities
{
    public class ModSettings : BaseEntity
    {
        [JsonIgnore] public bool ShowDetails { get; set; }

        public Games Game { get; set; }
        public string FileName { get; set; }
        public bool Enabled { get; set; }
    }
}