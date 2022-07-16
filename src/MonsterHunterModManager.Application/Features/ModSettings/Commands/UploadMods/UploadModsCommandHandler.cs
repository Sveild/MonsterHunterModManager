﻿using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;

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
    
    public Task<List<Domain.Entities.ModSettings>> Handle(UploadMods.UploadModsCommand request, CancellationToken cancellationToken)
    {
        var gameSettings = _applicationPersistenceContext.GetGameSettings(request.Game);
        
        foreach(var file in request.Files)
        {
            _physicalFileService.UploadFile(gameSettings, file);
                
            var modSettings = new Domain.Entities.ModSettings
            {
                Game = request.Game,
                ShowDetails = false,
                FileName = Path.GetFileName(file),
            };
            
            _applicationPersistenceContext.Save(modSettings);
        }

        return Task.FromResult(_applicationPersistenceContext.GetModsSettings(request.Game));
    }
}