using MonsterHunterModManager.Domain.Common;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Domain.Events.ModSettings;

public class ModDisabledEvent : BaseEvent
{
    public Games Game { get; }

    public ModDisabledEvent(Games game)
    {
        Game = game;
    }
}