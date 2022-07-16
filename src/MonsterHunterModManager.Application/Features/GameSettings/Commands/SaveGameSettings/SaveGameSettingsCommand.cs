using MediatR;

namespace MonsterHunterModManager.Application.Features.GameSettings.Commands.SaveGameSettings;

public class SaveGameSettingsCommand : IRequest<Domain.Entities.GameSettings>
{
    public Domain.Entities.GameSettings Settings { get; set; }
}