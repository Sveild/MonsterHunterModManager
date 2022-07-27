using MediatR;
using MonsterHunterModManager.Domain.Common;

namespace MonsterHunterModManager.Infrastructure.Common;

public static class MediatorExtension
{
    public static async Task DispatchDomainEvent(this IMediator mediator, BaseEntity entity)
    {
        foreach (var domainEvent in entity.DomainEvents)
            await mediator.Publish(domainEvent);
    }
}