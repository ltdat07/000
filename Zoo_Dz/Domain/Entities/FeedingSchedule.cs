using System;
using Zoo_Dz.Domain.ValueObjects;

namespace Zoo_Dz.Domain.Entities
{
    public class FeedingSchedule
    {
        public Guid Id { get; private set; }
        public Guid AnimalId { get; private set; }

        /// <summary>
        /// Время кормления (дата+время).
        /// </summary>
        public DateTime ScheduledTime { get; private set; }

        public FoodType FoodType { get; private set; }

        /// <summary>
        /// Отмечено ли кормление как выполненное.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Время фактического кормления.
        /// </summary>
        public DateTime? CompletedAt { get; private set; }

        public FeedingSchedule(Guid id, Guid animalId, DateTime scheduledTime, FoodType foodType)
        {
            Id = id;
            AnimalId = animalId;
            ScheduledTime = scheduledTime;
            FoodType = foodType;
            IsCompleted = false;
        }

        /// <summary>
        /// Перенести кормление на новое время.
        /// </summary>
        public void Reschedule(DateTime newTime)
        {
            if (newTime == ScheduledTime)
                return;

            if (IsCompleted)
                throw new InvalidOperationException("Cannot reschedule a feeding that has already been completed.");

            ScheduledTime = newTime;
        }

        /// <summary>
        /// Отметить кормление выполненным.
        /// </summary>
        public void MarkAsDone()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Feeding is already marked as done.");

            IsCompleted = true;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
