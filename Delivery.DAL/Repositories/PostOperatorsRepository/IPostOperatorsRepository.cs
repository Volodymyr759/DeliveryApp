using System.Collections.Generic;
using Delivery.DAL.Models;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Postal operators repo interface
    /// </summary>
    public interface IPostOperatorsRepository
    {
        /// <summary>
        /// Creates the new postal operator
        /// </summary>
        /// <param name="postOperator">Instance of the postal operator</param>
        void Create(IPostOperator postOperator);

        /// <summary>
        /// Returns the list of postal operators
        /// </summary>
        /// <returns>The list of postal operators</returns>
        IEnumerable<IPostOperator> GetAll();

        /// <summary>
        /// Returns postal operator by Id
        /// </summary>
        /// <param name="postOperatorId">PostOperator Id</param>
        /// <returns>Instance of the postal operator</returns>
        IPostOperator GetById(int postOperatorId);

        /// <summary>
        /// Updates postal operator
        /// </summary>
        /// <param name="postOperator">Instance of the postal operator</param>
        void Update(IPostOperator postOperator);
    }
}