using System;

namespace Zoo_Dz.Application.Services
{
    /// <summary>
    /// Сервис для перемещения животных между вольерами.
    /// </summary>
    public interface IAnimalTransferService
    {
        /// <summary>
        /// Переместить животное в новый вольер.
        /// </summary>
        /// <param name="animalId">ID животного</param>
        /// <param name="targetEnclosureId">ID целевого вольера</param>
        void TransferAnimal(Guid animalId, Guid targetEnclosureId);
    }
}
