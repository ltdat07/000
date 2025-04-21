using System;
using System.Reflection;
using Zoo_Dz.Domain.ValueObjects;

namespace Zoo_Dz.Domain.Entities
{
    public class Animal
    {
        public Guid Id { get; private set; }
        public string Species { get; private set; }    // Вид
        public string Name { get; private set; }       // Кличка
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public FoodType FavoriteFood { get; private set; }
        public HealthStatus Status { get; private set; }

        // Вольер, в котором находится животное
        public Guid EnclosureId { get; private set; }

        // Время последнего кормления
        public DateTime? LastFedAt { get; private set; }

        public Animal(
            Guid id,
            string species,
            string name,
        DateTime birthDate,
            Gender gender,
            FoodType favoriteFood,
            Guid enclosureId)
        {
            Id = id;
            Species = species;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            Status = HealthStatus.Healthy;
            EnclosureId = enclosureId;
        }

        /// <summary>
        /// Кормим животное: если еда не совпадает с любимой,
        /// животное откажется есть.
        /// </summary>
        public void Feed(FoodType food)
        {
            if (food != FavoriteFood)
                throw new InvalidOperationException(
                    $"Animal '{Name}' (вид: {Species}) refuses to eat {food}");

            LastFedAt = DateTime.UtcNow;
        }

        public void Heal()
        {
            Status = HealthStatus.Healthy;
        }

        public void MarkAsSick()
        {
            Status = HealthStatus.Sick;
        }

        public void MoveTo(Guid newEnclosureId)
        {
            EnclosureId = newEnclosureId;
        }
    }
}
