using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Entities;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;

public class UploadModsCommandHandler : IRequestHandler<UploadMods.UploadModsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;
    private readonly IPhysicalFileService _physicalFileService;

    public UploadModsCommandHandler(
        IApplicationPersistenceContext applicationPersistenceContext,
        IPhysicalFileService physicalFileService
    )
    {
        _applicationPersistenceContext = applicationPersistenceContext;
        _physicalFileService = physicalFileService;
    }
    
    public async Task<List<Domain.Entities.ModSettings>> Handle(UploadMods.UploadModsCommand request, CancellationToken cancellationToken)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        foreach(var file in request.Files)
        {
            _physicalFileService.UploadFile(gameSettings, file);

            var modSettings = new Domain.Entities.ModSettings
            {
                Id = Guid.NewGuid(),
                Game = request.Game,
                ShowDetails = false,
                ZipFileName = Path.GetFileName(file),
            };

            var files = _physicalFileService.GetFileNames(gameSettings, modSettings);
            modSettings.ModFilesSettings = files.Select(f => new ModFileSettings { OriginalName = f }).ToList();
            
            await _applicationPersistenceContext.Save(modSettings);
        }

        return _applicationPersistenceContext.GetModsSettings(request.Game);
    }
}