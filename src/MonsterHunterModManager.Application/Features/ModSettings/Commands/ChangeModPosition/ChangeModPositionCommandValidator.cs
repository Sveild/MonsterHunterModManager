using FluentValidation;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Domain.Enums;

namespace MonsterHunterModManager.Application.Features.ModSettings.Commands.ChangeModPositionCommand;

public class ChangeModPositionCommandValidator : AbstractValidator<ChangeModPositionCommand>
{
    private readonly IApplicationPersistenceContext _applicationPersistenceContext;

    public ChangeModPositionCommandValidator(IApplicationPersistenceContext _applicationPersistenceContext)
    {
        this._applicationPersistenceContext = _applicationPersistenceContext;
        
        RuleFor(x => x.Game).NotEmpty().NotEqual(Games.None);
        RuleFor(x => x.Id).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Move).NotEmpty().Must(CheckMove);
    }

    private bool CheckMove(ChangeModPositionCommand command, int move)
    {
        var modSettings = _applicationPersistenceContext.GetModsSettings(command.Game).Single(ms => ms.Id == command.Id);
        return modSettings.Position - move > 0;
    }
}