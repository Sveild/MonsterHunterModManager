using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;

namespace MonsterHunterModManager.Application.Features.GameSettings.Commands.SaveGameSettings;

public class SaveGameSettingsCommandHandler : IRequestHandler<SaveGameSettingsCommand, Domain.Entities.GameSettings>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public SaveGameSettingsCommandHandler(IApplicationPersistenceContext applicationPersistenceContext)
    {
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public async Task<Domain.Entities.GameSettings> Handle(SaveGameSettingsCommand request, CancellationToken cancellationToken)
    {
        await _applicationPersistenceContext.Save(request.Settings);
        return request.Settings;
    }
}