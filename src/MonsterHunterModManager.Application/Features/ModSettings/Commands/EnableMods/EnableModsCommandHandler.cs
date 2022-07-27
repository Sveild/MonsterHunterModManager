using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.EnableMods;

public class EnableModsCommandHandler : IRequestHandler<EnableModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IMediator _mediator;
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public EnableModsCommandHandler(
        IMediator mediator,
        IApplicationPersistenceContext applicationPersistenceContext
    )
    {
        _mediator = mediator;
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public async Task<List<Domain.Entities.ModSettings>> Handle(EnableModsCommand request, CancellationToken cancellationToken)
    {
        foreach (var modSettings in request.ModsSettings)
            modSettings.Enabled = true;

        await _applicationPersistenceContext.SaveMultiple(request.ModsSettings);
        await _mediator.Publish(new ModEnabledEvent(request.Game), cancellationToken);
        return _applicationPersistenceContext.GetModsSettings(request.Game);
    }
}