using System;
using System.Collections.Generic;
using Zoo_Dz.Domain.Entities;

namespace Zoo_Dz.Domain.Repositories
{
    public interface IEnclosureRepository
    {
        /// <summary>
        /// Получить вольер по его идентификатору.
        /// </summary>
        Enclosure GetById(Guid id);

        /// <summary>
        /// Получить все вольеры.
        /// </summary>
        IEnumerable<Enclosure> GetAll();

        /// <summary>
        /// Добавить новый вольер.
        /// </summary>
        void Add(Enclosure enclosure);

        /// <summary>
        /// Удалить вольер по идентификатору.
        /// </summary>
        void Remove(Guid id);
    }
}
