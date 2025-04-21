using System;
using System.Collections.Generic;
using Zoo_Dz.Domain.Entities;

namespace Zoo_Dz.Domain.Repositories
{
    public interface IAnimalRepository
    {
        /// <summary>
        /// Получить животное по его идентификатору.
        /// </summary>
        Animal GetById(Guid id);

        /// <summary>
        /// Получить всех животных.
        /// </summary>
        IEnumerable<Animal> GetAll();

        /// <summary>
        /// Добавить новое животное.
        /// </summary>
        void Add(Animal animal);

        /// <summary>
        /// Удалить животное по идентификатору.
        /// </summary>
        void Remove(Guid id);
    }
}
