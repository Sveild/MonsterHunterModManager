using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;

public class DisableModsCommand : IRequest<List<Domain.Entities.ModSettings>>
{
    public Games Game { get; set; }
    public List<Domain.Entities.ModSettings> ModsSettings { get; set; }
}