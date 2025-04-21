using System;
using Zoo_Dz.Application.Ports.Events;
using Zoo_Dz.Domain.Entities;
using Zoo_Dz.Domain.Events;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Application.Services
{
    public class FeedingOrganizationService : IFeedingOrganizationService
    {
        private readonly IFeedingScheduleRepository _scheduleRepo;
        private readonly IAnimalRepository _animalRepo;
        private readonly IDomainEventPublisher _eventPublisher;

        public FeedingOrganizationService(
            IFeedingScheduleRepository scheduleRepo,
            IAnimalRepository animalRepo,
            IDomainEventPublisher eventPublisher)
        {
            _scheduleRepo = scheduleRepo;
            _animalRepo = animalRepo;
            _eventPublisher = eventPublisher;
        }

        public void PerformFeeding(Guid scheduleId)
        {
            // 1. Получаем запись расписания
            var schedule = _scheduleRepo.GetById(scheduleId);

            // 2. Проверяем, не выполнено ли уже
            if (schedule.IsCompleted)
                throw new InvalidOperationException($"Feeding schedule {scheduleId} is already completed.");

            // 3. Убедимся, что сейчас — или позже — время кормления
            if (DateTime.UtcNow < schedule.ScheduledTime)
                throw new InvalidOperationException(
                    $"Too early to feed (scheduled at {schedule.ScheduledTime:O}).");

            // 4. Достаём животное и кормим
            var animal = _animalRepo.GetById(schedule.AnimalId);
            animal.Feed(schedule.FoodType);

            // 5. Отмечаем кормление выполненным
            schedule.MarkAsDone();

            // 6. Публикуем событие
            var evt = new FeedingTimeEvent(schedule.Id, animal.Id, schedule.ScheduledTime);
            _eventPublisher.Publish(evt);
        }

        public void RescheduleFeeding(Guid scheduleId, DateTime newTime)
        {
            var schedule = _scheduleRepo.GetById(scheduleId);
            schedule.Reschedule(newTime);
        }
    }
}
