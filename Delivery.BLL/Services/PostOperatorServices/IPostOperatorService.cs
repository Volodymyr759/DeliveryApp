using Delivery.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Сервіс управління поштовими операторами
    /// </summary>
    public interface IPostOperatorService
    {
        /// <summary>
        /// Створення адміністратором нового поштового оператора - додається після програмної реалізації кожного нового оператора
        /// </summary>
        /// <param name="postOperatorDto">Модель Dto поштового оператора</param>
        void Add(PostOperatorDto postOperatorDto);

        /// <summary>
        /// Повертає екземпляр поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Екземпляр поштового оператора</returns>
        PostOperatorDto GetById(int postOperatorId);

        /// <summary>
        /// Повертає список усіх реалізованих в системі Delivery поштових операторів
        /// </summary>
        /// <returns>Список реалізованих операторів</returns>
        IEnumerable<PostOperatorDto> GetAll();

        /// <summary>
        /// Оновлює дані поштового оператора
        /// </summary>
        /// <param name="postOperatorDto">Екземпляр Dto поштового оператора</param>
        void UpdatePostOperator(PostOperatorDto postOperatorDto);
    }
}
