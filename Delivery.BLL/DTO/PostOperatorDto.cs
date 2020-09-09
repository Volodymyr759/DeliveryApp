namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Dto модель поштового оператора
    /// </summary>
    public class PostOperatorDto
    {
        /// <summary>
        /// Postal operator Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Postal operator name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Search page for tracking shipments by number in the information system of the postal operator.
        /// </summary>
        public string LinkToSearchPage { get; set; }

        /// <summary>
        /// The path to the location of the image of the postal operator's logo on the host server of the Delivery service.
        /// </summary>
        public string PathToLogoImage { get; set; }

        /// <summary>
        /// Indicates whether the operator is available for use. The administrator can deactivate the operator in case of changes in access conditions that have not yet been implemented in the Delivery system.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
    }
}
