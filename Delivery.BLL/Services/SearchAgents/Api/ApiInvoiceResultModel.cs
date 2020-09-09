using System.Collections.Generic;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// A class adapted to the response format of the Nova Poshta API service
    /// </summary>
    public class InvoiceResultModel
    {
        /// <summary>
        /// List of invoices returned in response to the Nova Poshta API service
        /// </summary>
        public List<ApiInvoicesModel> Data { get; set; }
    }
}
