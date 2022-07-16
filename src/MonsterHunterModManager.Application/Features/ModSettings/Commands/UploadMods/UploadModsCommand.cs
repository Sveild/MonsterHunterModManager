using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;

public class UploadModsCommand : IRequest<List<Domain.Entities.ModSettings>>
{
    public Games Game { get; set; }
    public List<string> Files { get; set; }
}