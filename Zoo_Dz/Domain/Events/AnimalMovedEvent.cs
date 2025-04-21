using System;

namespace Zoo_Dz.Domain.Events
{
    /// <summary>
    /// Событие, возникающее при перемещении животного из одного вольера в другой.
    /// </summary>
    public class AnimalMovedEvent : IDomainEvent
    {
        public Guid AnimalId { get; }
        public Guid FromEnclosureId { get; }
        public Guid ToEnclosureId { get; }

        /// <summary>
        /// Время генерации события (UTC).
        /// </summary>
        public DateTime OccurredAt { get; }

        public AnimalMovedEvent(Guid animalId, Guid fromEnclosureId, Guid toEnclosureId)
        {
            AnimalId = animalId;
            FromEnclosureId = fromEnclosureId;
            ToEnclosureId = toEnclosureId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
