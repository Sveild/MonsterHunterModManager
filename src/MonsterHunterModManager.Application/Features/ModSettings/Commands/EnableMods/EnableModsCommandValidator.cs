using FluentValidation;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.EnableMods;

public class EnableModsCommandValidator : AbstractValidator<EnableModsCommand>
{
    public EnableModsCommandValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
        RuleFor(x => x.ModsSettings).NotEmpty();
    }
}