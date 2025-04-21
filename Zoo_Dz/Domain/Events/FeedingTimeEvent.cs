using System;

namespace Zoo_Dz.Domain.Events
{
    /// <summary>
    /// Событие, возникающее в момент наступления времени кормления по расписанию.
    /// </summary>
    public class FeedingTimeEvent : IDomainEvent
    {
        public Guid ScheduleId { get; }
        public Guid AnimalId { get; }
        public DateTime ScheduledTime { get; }

        /// <summary>
        /// Время генерации события (UTC).
        /// </summary>
        public DateTime OccurredAt { get; }

        public FeedingTimeEvent(Guid scheduleId, Guid animalId, DateTime scheduledTime)
        {
            ScheduleId = scheduleId;
            AnimalId = animalId;
            ScheduledTime = scheduledTime;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
