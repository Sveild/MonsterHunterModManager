using MediatR;
using MonsterHunterModManager.Application.Common.Interfaces;

namespace MonsterHunterModManager.Application.Features.ModSettings.Queries.GetModsSettings;

public class GetModsSettingsCommandHandler : IRequestHandler<GetModsSettingsCommand, List<Domain.Entities.ModSettings>>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public GetModsSettingsCommandHandler(IApplicationPersistenceContext applicationPersistenceContext)
    {
        _applicationPersistenceContext = applicationPersistenceContext;
    }
    
    public Task<List<Domain.Entities.ModSettings>> Handle(GetModsSettingsCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_applicationPersistenceContext.GetModsSettings(request.Game));
    }
}