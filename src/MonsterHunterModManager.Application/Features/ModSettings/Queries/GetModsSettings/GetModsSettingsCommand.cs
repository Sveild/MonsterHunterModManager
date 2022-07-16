using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Queries.GetModsSettings;

public class GetModsSettingsCommand : IRequest<List<Domain.Entities.ModSettings>>
{
    public Games Game { get; set; }
}