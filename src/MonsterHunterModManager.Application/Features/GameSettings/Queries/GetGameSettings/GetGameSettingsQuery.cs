using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.GameSettings.Queries.GetGameSettings;

public class GetGameSettingsQuery : IRequest<Domain.Entities.GameSettings>
{
    public Games Game { get; set; }
}