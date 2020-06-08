namespace Delivery.BLL.Services
{
    /// <summary>
    /// Допоміжний клас для налаштування доступу до API-сервісу Нової Пошти
    /// </summary>
    public class ApiDocument
    {
        /// <summary>
        /// Ключ доступу, генерується кожних 24 год. API-сервісом Нової Пошти. В с-мі Delivery передбачається,
        /// що адміністратор щоденно оновлює цей ключ доступу в Web.config
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Назва json-моделі доступу в API-сервісі Нової Пошти
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Назва методу API-сервісу Нової Пошти
        /// </summary>
        public string CalledMethod { get; set; }

        /// <summary>
        /// Налаштування запиту в API-сервісі Нової Пошти 
        /// </summary>
        public ApiMethodProperties MethodProperties { get; set; }
    }
}
