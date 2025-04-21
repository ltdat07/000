using System;
using Zoo_Dz.Domain.ValueObjects;

namespace Zoo_Dz.Domain.Entities
{
    public class Enclosure
    {
        public Guid Id { get; private set; }
        public EnclosureType Type { get; private set; }
        public double SizeSqM { get; private set; }      // Площадь в м²
        public int Capacity { get; private set; }        // Максимальная вместимость
        public int CurrentCount { get; private set; }    // Текущее число животных

        // Для истории уборок (необязательно, но полезно)
        public DateTime? LastCleanedAt { get; private set; }

        public Enclosure(Guid id, EnclosureType type, double sizeSqM, int capacity)
        {
            Id = id;
            Type = type;
            SizeSqM = sizeSqM;
            Capacity = capacity;
            CurrentCount = 0;
        }

        /// <summary>
        /// Добавить животное: проверяем вместимость.
        /// </summary>
        public void AddAnimal()
        {
            if (CurrentCount >= Capacity)
                throw new InvalidOperationException(
                    $"Enclosure {Id} is full (capacity = {Capacity})");

            CurrentCount++;
        }

        /// <summary>
        /// Убрать животное: проверяем, что есть кого убирать.
        /// </summary>
        public void RemoveAnimal()
        {
            if (CurrentCount <= 0)
                throw new InvalidOperationException(
                    $"Enclosure {Id} is already empty");

            CurrentCount--;
        }

        /// <summary>
        /// Провести уборку — обновляем дату.
        /// </summary>
        public void Clean()
        {
            LastCleanedAt = DateTime.UtcNow;
        }
    }
}
