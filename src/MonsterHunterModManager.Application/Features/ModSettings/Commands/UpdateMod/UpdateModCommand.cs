using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.UpdateMod;

public class UpdateModCommand : IRequest
{
    public Domain.Entities.ModSettings ModSettings { get; set; }
    public Games Game { get; set; }
    public string File { get; set; }
}