using System;
using System.Collections.Generic;
using Zoo_Dz.Domain.Entities;

namespace Zoo_Dz.Domain.Repositories
{
    public interface IFeedingScheduleRepository
    {
        /// <summary>
        /// Получить расписание кормления по идентификатору.
        /// </summary>
        FeedingSchedule GetById(Guid id);

        /// <summary>
        /// Получить все записи в расписании.
        /// </summary>
        IEnumerable<FeedingSchedule> GetAll();

        /// <summary>
        /// Добавить новую запись в расписание кормления.
        /// </summary>
        void Add(FeedingSchedule schedule);

        /// <summary>
        /// Удалить запись из расписания по идентификатору.
        /// </summary>
        void Remove(Guid id);
    }
}
