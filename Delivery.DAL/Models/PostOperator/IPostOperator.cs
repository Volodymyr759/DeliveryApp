namespace Delivery.DAL.Models
{
    /// <summary>
    /// Postal operator's interface
    /// </summary>
    public interface IPostOperator
    {
        /// <summary>
        /// Postal operator Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Postal operator name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Search page for tracking shipments by number in the information system of the postal operator.
        /// </summary>
        string LinkToSearchPage { get; set; }

        /// <summary>
        /// The path to the location of the image of the postal operator's logo on the host server of the Delivery service
        /// </summary>
        string PathToLogoImage { get; set; }

        /// <summary>
        /// Indicates whether the operator is available for use. The administrator can deactivate the operator in case of changes in access conditions that have not yet been implemented in the Delivery system.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        string Notes { get; set; }
    }
}