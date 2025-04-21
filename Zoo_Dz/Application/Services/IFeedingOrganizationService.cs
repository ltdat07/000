using System;

namespace Zoo_Dz.Application.Services
{
    /// <summary>
    /// Сервис организации кормлений:
    /// • выполнить кормление по расписанию
    /// • перенести время кормления
    /// </summary>
    public interface IFeedingOrganizationService
    {
        /// <summary>
        /// Выполнить кормление по записи расписания.
        /// </summary>
        /// <param name="scheduleId">ID записи расписания</param>
        void PerformFeeding(Guid scheduleId);

        /// <summary>
        /// Перенести время кормления.
        /// </summary>
        /// <param name="scheduleId">ID записи расписания</param>
        /// <param name="newTime">Новое время (UTC)</param>
        void RescheduleFeeding(Guid scheduleId, DateTime newTime);
    }
}
