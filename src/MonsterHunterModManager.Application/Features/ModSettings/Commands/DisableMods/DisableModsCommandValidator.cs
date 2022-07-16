using FluentValidation;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;

public class DisableModsCommandValidator : AbstractValidator<DisableModsCommand>
{
    public DisableModsCommandValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
        RuleFor(x => x.ModsSettings).NotEmpty();
    }
}