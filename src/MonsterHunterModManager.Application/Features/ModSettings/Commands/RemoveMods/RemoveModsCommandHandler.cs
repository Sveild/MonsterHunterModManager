using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;

public class RemoveModsCommandHandler : IRequestHandler<RemoveModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IMediator _mediator;
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;

    public RemoveModsCommandHandler(
        IMediator mediator,
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    )
    {
        _mediator = mediator;
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }
    
    public async Task<List<Domain.Entities.ModSettings>> Handle(RemoveModsCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DisableModsCommand {Game = request.Game, ModsSettings = request.ModsSettings}, cancellationToken);
        
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        foreach (var modSettings in request.ModsSettings)
        {
            _physicalFileService.DeleteModFile(gameSettings, modSettings);
            _applicationPersistenceContext.RemoveModSettings(modSettings);
        }
        
        return _applicationPersistenceContext.GetModsSettings(request.Game);
    }
}