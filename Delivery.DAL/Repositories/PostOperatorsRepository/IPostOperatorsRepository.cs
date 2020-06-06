using System.Collections.Generic;
using Delivery.DAL.Models;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Репозиторій поштових операторів
    /// </summary>
    public interface IPostOperatorsRepository
    {
        /// <summary>
        /// Створює нового поштового оператора (при відповідній програмній реалізації)
        /// </summary>
        /// <param name="postOperator">Екземпляр поштового оператора</param>
        void Create(IPostOperator postOperator);

        /// <summary>
        /// Повертає реалізовані на сервісі Delivery поштові оператори
        /// </summary>
        /// <returns>Список поштових операторів</returns>
        IEnumerable<IPostOperator> GetAll();

        /// <summary>
        /// Повертає екземпляр поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Eкземпляр поштового оператора</returns>
        IPostOperator GetById(int postOperatorId);

        /// <summary>
        /// Оновлює запис поштового оператора
        /// </summary>
        /// <param name="postOperator">Екземпляр поштового оператора</param>
        void Update(IPostOperator postOperator);
    }
}