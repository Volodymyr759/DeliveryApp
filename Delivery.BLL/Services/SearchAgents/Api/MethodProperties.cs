namespace Delivery.BLL.Services
{
    /// <summary>
    /// Клас налаштувань запиту в API-сервісі Нової Пошти
    /// </summary>
    public class ApiMethodProperties
    {
        /// <summary>
        /// Початкова дата запитуваного періоду
        /// </summary>
        public string DateTimeFrom { get; set; }

        /// <summary>
        /// Кінцева дата запитуваного періоду
        /// </summary>
        public string DateTimeTo { get; set; }

        /// <summary>
        /// Параметр, який вказує необхідність отримання повного списку накладних
        /// </summary>
        public string GetFullList { get; set; } 
    }
}
