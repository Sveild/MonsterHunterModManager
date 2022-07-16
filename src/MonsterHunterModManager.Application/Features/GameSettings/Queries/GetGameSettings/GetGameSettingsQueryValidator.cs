using FluentValidation;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.GameSettings.Queries.GetGameSettings;

public class GetGameSettingsQueryValidator : AbstractValidator<GetGameSettingsQuery>
{
    public GetGameSettingsQueryValidator()
    {
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
    }
}