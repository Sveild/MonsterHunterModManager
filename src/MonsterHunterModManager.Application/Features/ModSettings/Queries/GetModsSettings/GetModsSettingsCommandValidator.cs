using FluentValidation;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Queries.GetModsSettings;

public class GetModsSettingsCommandValidator : AbstractValidator<GetModsSettingsCommand>
{
    public GetModsSettingsCommandValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
    }
}