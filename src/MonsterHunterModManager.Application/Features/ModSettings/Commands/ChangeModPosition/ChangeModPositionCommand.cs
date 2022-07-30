using MediatR;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.ChangeModPositionCommand;

public class ChangeModPositionCommand : IRequest
{
    public Games Game { get; set; }
    public Guid Id { get; set; }
    public int Move { get; set; }
}