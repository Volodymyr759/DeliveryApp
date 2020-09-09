using System;
using System.Collections.Generic;
using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.BLL.Validators;
using Delivery.DAL.Models;
using Delivery.DAL.Repositories;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Postal operators management service
    /// </summary>
    public class PostOperatorService : IPostOperatorService
    {
        private readonly string connectionString;

        private readonly IPostOperatorsRepository postOperatorsRepository;

        private PostOperatorsValidator postOperatorsValidator = new PostOperatorsValidator();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="postOperatorsRepository">Object of the repository of postal operators</param>
        public PostOperatorService(string connectionString, IPostOperatorsRepository postOperatorsRepository)
        {
            this.connectionString = connectionString;
            this.postOperatorsRepository = postOperatorsRepository;
        }

        /// <summary>
        /// Creation of a new postal operator by the administrator - added after the software implementation of each new operator
        /// </summary>
        /// <param name="postOperatorDto">The Dto model of the postal operator</param>
        public void Add(PostOperatorDto postOperatorDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorDto, PostOperator>()).CreateMapper();
            PostOperator postOperator = mapper.Map<PostOperator>(postOperatorDto);

            var results = postOperatorsValidator.Validate(postOperator);
            if (results.IsValid)
            {
                postOperatorsRepository.Create(postOperator);
            }
            else
            {
                throw new Exception("Помилка валідації поштового оператора: " + Environment.NewLine +
                    ValidationResultsHelper.GetValidationErrors(results));
            }
        }

        /// <summary>
        /// Returns an instance of the postal operator by Id
        /// </summary>
        /// <param name="postOperatorId">Postal operator Id</param>
        /// <returns>Instance of the postal operator</returns>
        public PostOperatorDto GetById(int postOperatorId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperator, PostOperatorDto>()).CreateMapper();

            return mapper.Map<PostOperatorDto>(postOperatorsRepository.GetById(postOperatorId));
        }

        /// <summary>
        /// Returns a list of all postal operators implemented in the Delivery system
        /// </summary>
        /// <returns>List of postal operators</returns>
        public IEnumerable<PostOperatorDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperator, PostOperatorDto>()).CreateMapper();

            return mapper.Map<IEnumerable<PostOperatorDto>>(postOperatorsRepository.GetAll());
        }

        /// <summary>
        /// Updates the data of the postal operator
        /// </summary>
        /// <param name="postOperatorDto">Instance Dto the postal operator</param>
        public void UpdatePostOperator(PostOperatorDto postOperatorDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperatorDto, PostOperator>()).CreateMapper();
            PostOperator postOperator = mapper.Map<PostOperator>(postOperatorDto);

            var results = postOperatorsValidator.Validate(postOperator);
            if (results.IsValid)
            {
                postOperatorsRepository.Update(postOperator);
            }
            else
            {
                throw new Exception("Помилка валідації поштового оператора: " + Environment.NewLine +
                    ValidationResultsHelper.GetValidationErrors(results));
            }
        }
    }
}
