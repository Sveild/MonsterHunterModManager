using FluentValidation;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;

public class RemoveModsCommandValidator : AbstractValidator<RemoveModsCommand>
{
    public RemoveModsCommandValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
    }
}