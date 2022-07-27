using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.EventHandler;

public class ModEnabledEventHandler : ModToggleBase, INotificationHandler<ModEnabledEvent>
{
    public ModEnabledEventHandler(
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    ) : base(applicationPersistenceContext, physicalFileService)
    {
    }
    
    public async Task Handle(ModEnabledEvent notification, CancellationToken cancellationToken)
    {
        DisableAllMods(notification.Game);
        EnableAllMods(notification.Game);
    }
}