using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Events.ModSettings;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.UpdateMod;

public class UpdateModCommandHandler : IRequestHandler<UpdateModCommand>
{
    private readonly IMediator _mediator;
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;

    public UpdateModCommandHandler(
        IMediator mediator,
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    )
    {
        _mediator = mediator;
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }
    
    public async Task<Unit> Handle(UpdateModCommand request, CancellationToken cancellationToken)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        _physicalFileService.UploadFile(gameSettings, request.File);

        request.ModSettings.ZipFileName = Path.GetFileName(request.File);

        var files = _physicalFileService.GetFileNames(gameSettings, request.ModSettings);
        request.ModSettings.ModFilesSettings = files.Select(f => new ModFileSettings { OriginalName = f }).ToList();
            
        await _applicationPersistenceContext.Save(request.ModSettings);

        if (request.ModSettings.Enabled)
            await _mediator.Publish(new ModEnabledEvent(request.Game));
        
        return default;
    }
}