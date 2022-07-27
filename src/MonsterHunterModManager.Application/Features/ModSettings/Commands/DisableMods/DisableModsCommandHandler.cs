using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;

public class DisableModsCommandHandler : IRequestHandler<DisableModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IMediator _mediator;
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public DisableModsCommandHandler(
        IMediator mediator,
        IApplicationPersistenceContext applicationPersistenceContext
    )
    {
        _mediator = mediator;
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public async Task<List<Domain.Entities.ModSettings>> Handle(DisableModsCommand request, CancellationToken cancellationToken)
    {
        foreach (var modSettings in request.ModsSettings)
            modSettings.Enabled = false;

        await _applicationPersistenceContext.SaveMultiple(request.ModsSettings);
        await _mediator.Publish(new ModDisabledEvent(request.Game), cancellationToken);
        return _applicationPersistenceContext.GetModsSettings(request.Game);
    }
}