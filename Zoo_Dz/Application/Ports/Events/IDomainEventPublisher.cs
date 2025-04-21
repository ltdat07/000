using Zoo_Dz.Domain.Events;

namespace Zoo_Dz.Application.Ports.Events
{
    /// <summary>
    /// Публикует доменные события (например, AnimalMovedEvent).
    /// </summary>
    public interface IDomainEventPublisher
    {
        void Publish(IDomainEvent domainEvent);
    }
}
