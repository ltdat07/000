using System;
using Zoo_Dz.Application.Ports.Events;
using Zoo_Dz.Domain.Events;

namespace Zoo_Dz.Infrastructure.Events
{
    /// <summary>
    /// Простейшая реализация IDomainEventPublisher:
    /// выводит событие в консоль.
    /// </summary>
    public class SimpleDomainEventPublisher : IDomainEventPublisher
    {
        public void Publish(IDomainEvent domainEvent)
        {
            // Здесь можно интегрировать реальный EventBus, логирование и т.п.
            Console.WriteLine(
                $"[Domain Event] {domainEvent.GetType().Name} occurred at {DateTime.UtcNow:O}");
        }
    }
}
