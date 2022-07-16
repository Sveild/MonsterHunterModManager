using FluentValidation;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;

public class UploadModsCommandValidator : AbstractValidator<UploadMods.UploadModsCommand>
{
    public UploadModsCommandValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
        RuleFor(x => x.Files).NotEmpty();
    }
}