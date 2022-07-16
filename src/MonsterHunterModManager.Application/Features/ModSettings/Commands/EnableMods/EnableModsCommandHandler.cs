using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.EnableMods;

public class EnableModsCommandHandler : IRequestHandler<EnableModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;

    public EnableModsCommandHandler(
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    )
    {
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }
    
    public Task<List<Domain.Entities.ModSettings>> Handle(EnableModsCommand request, CancellationToken cancellationToken)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        foreach (var modSettings in request.ModsSettings)
        {
            if (!_physicalFileService.IsModeEnabled(gameSettings,  modSettings))
                _physicalFileService.EnableMod(gameSettings, modSettings);
            
            modSettings.Enabled = true;
            _applicationPersistenceContext.Save(modSettings);
        }
        
        return Task.FromResult(_applicationPersistenceContext.GetModsSettings(request.Game));
    }
}