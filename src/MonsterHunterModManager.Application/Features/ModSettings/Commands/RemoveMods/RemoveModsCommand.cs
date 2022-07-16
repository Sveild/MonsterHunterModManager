using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;

public class RemoveModsCommand : IRequest<List<Domain.Entities.ModSettings>>
{
    public Games Game { get; set; }
    public List<Domain.Entities.ModSettings> ModsSettings { get; set; }
}