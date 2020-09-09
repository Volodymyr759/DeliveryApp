using Delivery.BLL.DTO;
using System.Collections.Generic;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Postal operators management service interface
    /// </summary>
    public interface IPostOperatorService
    {
        /// <summary>
        /// Creation of a new postal operator by the administrator - added after the software implementation of each new operatorного нового оператора
        /// </summary>
        /// <param name="postOperatorDto">The Dto model of the postal operator</param>
        void Add(PostOperatorDto postOperatorDto);

        /// <summary>
        /// Returns an instance of the postal operator by Id
        /// </summary>
        /// <param name="postOperatorId">Postal operator Id</param>
        /// <returns>Instance of the postal operator</returns>
        PostOperatorDto GetById(int postOperatorId);

        /// <summary>
        /// Returns a list of all postal operators implemented in the Delivery system
        /// </summary>
        /// <returns>List of postal operators</returns>
        IEnumerable<PostOperatorDto> GetAll();

        /// <summary>
        /// Updates the data of the postal operators
        /// </summary>
        /// <param name="postOperatorDto">Instance Dto the postal operator</param>
        void UpdatePostOperator(PostOperatorDto postOperatorDto);
    }
}
