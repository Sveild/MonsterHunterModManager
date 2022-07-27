using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.EventHandler;

public class ModDisabledEventHandler : ModToggleBase, INotificationHandler<ModDisabledEvent>
{
    public ModDisabledEventHandler(
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    ) : base(applicationPersistenceContext, physicalFileService)
    {
    }
    
    public async Task Handle(ModDisabledEvent notification, CancellationToken cancellationToken)
    {
        DisableAllMods(notification.Game);
        EnableAllMods(notification.Game);
    }
}