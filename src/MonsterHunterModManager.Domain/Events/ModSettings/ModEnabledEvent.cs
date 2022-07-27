using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Domain.Events.ModSettings;

public class ModEnabledEvent : BaseEvent
{
    public Games Game { get; }

    public ModEnabledEvent(Games game)
    {
        Game = game;
    }
}