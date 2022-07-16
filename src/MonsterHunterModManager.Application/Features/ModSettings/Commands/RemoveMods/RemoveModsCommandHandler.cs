using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;

public class RemoveModsCommandHandler : IRequestHandler<RemoveModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;

    public RemoveModsCommandHandler(
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    )
    {
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }
    
    public Task<List<Domain.Entities.ModSettings>> Handle(RemoveModsCommand request, CancellationToken cancellationToken)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        foreach (var modSettings in request.ModsSettings)
        {
            _physicalFileService.DisableMod(gameSettings, modSettings);
            _physicalFileService.DeleteModFile(gameSettings, modSettings);
            _applicationPersistenceContext.RemoveModSettings(modSettings);
        }
        
        return Task.FromResult(_applicationPersistenceContext.GetModsSettings(request.Game));
    }
}