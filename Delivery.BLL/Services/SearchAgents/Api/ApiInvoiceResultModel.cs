using Delivery.BLL.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Клас, пристосований до формату відповіді API-сервісу Нової Пошти
    /// </summary>
    public class InvoiceResultModel
    {
        /// <summary>
        /// Список накладних, який повертається у відповіді API-сервісу Нової Пошти
        /// </summary>
        public List<ApiInvoicesModel> Data { get; set; }
    }
}
