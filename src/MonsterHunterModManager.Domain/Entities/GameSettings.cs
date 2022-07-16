using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Enums;
using MonsterHunterModManager.Domain.ValueObjects;

namespace MonsterHunterModManager.Domain.Entities
{
    public class GameSettings : BaseEntity
    {
        public Games Game { get; set; }
        public string GameDirectory { get; set; }
        public string ModsDirectory { get; set; } = DirectoryPath.ModsDirectory;
    }
}
