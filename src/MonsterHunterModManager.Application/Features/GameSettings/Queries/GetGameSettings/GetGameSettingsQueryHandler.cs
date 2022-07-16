using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.ValueObjects;

namespace MonsterHunterModManager.Application.Features.GameSettings.Queries.GetGameSettings;

public class GetGameSettingsQueryHandler : IRequestHandler<GetGameSettingsQuery, Domain.Entities.GameSettings>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public GetGameSettingsQueryHandler(IApplicationPersistenceContext applicationPersistenceContext)
    {
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public Task<Domain.Entities.GameSettings> Handle(GetGameSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = _applicationPersistenceContext.GetGameSettings(request.Game);

        if (settings.Game != request.Game)
        {
            settings.Game = request.Game;
            settings.ModsDirectory = DirectoryPath.ModsDirectory;
        }

        return Task.FromResult(settings);
    }
}