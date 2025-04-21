using System;
using Zoo_Dz.Application.Ports.Events;
using Zoo_Dz.Domain.Events;
using Zoo_Dz.Domain.Repositories;

namespace Zoo_Dz.Application.Services
{
    public class AnimalTransferService : IAnimalTransferService
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IEnclosureRepository _enclosureRepo;
        private readonly IDomainEventPublisher _eventPublisher;

        public AnimalTransferService(
            IAnimalRepository animalRepo,
            IEnclosureRepository enclosureRepo,
            IDomainEventPublisher eventPublisher)
        {
            _animalRepo = animalRepo;
            _enclosureRepo = enclosureRepo;
            _eventPublisher = eventPublisher;
        }

        public void TransferAnimal(Guid animalId, Guid targetEnclosureId)
        {
            // 1. Достать сущности
            var animal = _animalRepo.GetById(animalId);
            var fromEnclosure = _enclosureRepo.GetById(animal.EnclosureId);
            var toEnclosure = _enclosureRepo.GetById(targetEnclosureId);

            // 2. Проверить вместимость
            if (toEnclosure.CurrentCount >= toEnclosure.Capacity)
                throw new InvalidOperationException(
                    $"Целевой вольер {targetEnclosureId} заполнен (capacity={toEnclosure.Capacity})");

            // (здесь можно добавить логику совместимости типов вольеров)

            // 3. Убрать из старого и добавить в новый
            fromEnclosure.RemoveAnimal();
            toEnclosure.AddAnimal();

            // 4. Обновить само животное
            animal.MoveTo(targetEnclosureId);

            // 5. Сохранить изменения (при in-memory репозиториях объекты обновляются сами)

            // 6. Опубликовать доменное событие
            var evt = new AnimalMovedEvent(animalId, fromEnclosure.Id, toEnclosure.Id);
            _eventPublisher.Publish(evt);
        }
    }
}
