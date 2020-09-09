namespace Delivery.BLL.Services
{
    /// <summary>
    /// Auxiliary class for configuring access to the Nova Poshta API service
    /// </summary>
    public class ApiDocument
    {
        /// <summary>
        /// Access key, generated every 24 hours by Nova Poshta API service. The Delivery system assumes that the administrator updates this access key to Web.config daily
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The name of the json access model in the API service of Nova Poshta
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// The name of the method of the Nova Poshta API service
        /// </summary>
        public string CalledMethod { get; set; }

        /// <summary>
        /// Request settings in the Nova Poshta API service
        /// </summary>
        public ApiMethodProperties MethodProperties { get; set; }
    }
}
