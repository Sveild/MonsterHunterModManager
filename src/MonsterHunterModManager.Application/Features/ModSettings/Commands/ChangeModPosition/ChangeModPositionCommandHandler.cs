using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.ChangeModPositionCommand;

public class ChangeModPositionCommandHandler : IRequestHandler<ChangeModPositionCommand>
{
    private readonly IMediator _mediator;
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public ChangeModPositionCommandHandler(IMediator mediator, IApplicationPersistenceContext applicationPersistenceContext)
    {
        _mediator = mediator;
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public async Task<Unit> Handle(ChangeModPositionCommand request, CancellationToken cancellationToken)
    {
        var modsSettings = _applicationPersistenceContext.GetModsSettings(request.Game).OrderBy(ms => ms.Position).ToList();
        var newPos = modsSettings.Single(ms => ms.Id == request.Id).Position - request.Move;

        modsSettings.Single(ms => ms.Position == newPos).Position += request.Move;
        modsSettings.Single(ms => ms.Id == request.Id).Position -= request.Move;
        await _applicationPersistenceContext.SaveMultiple(modsSettings);

        await _mediator.Publish(new ModEnabledEvent(request.Game), cancellationToken);
        
        return default;
    }
}