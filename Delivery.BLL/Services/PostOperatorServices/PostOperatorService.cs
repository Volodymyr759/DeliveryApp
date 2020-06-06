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
    /// Сервіс управління поштовими операторами
    /// </summary>
    public class PostOperatorService : IPostOperatorService
    {
        private readonly string connectionString;

        private readonly IPostOperatorsRepository postOperatorsRepository;

        private PostOperatorsValidator postOperatorsValidator = new PostOperatorsValidator();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Строка підключення</param>
        /// <param name="postOperatorsRepository">Об'єкт репозиторію поштових операторів</param>
        public PostOperatorService(string connectionString, IPostOperatorsRepository postOperatorsRepository)
        {
            this.connectionString = connectionString;
            this.postOperatorsRepository = postOperatorsRepository;
        }

        /// <summary>
        /// Створення адміністратором нового поштового оператора - додається після програмної реалізації кожного нового оператора
        /// </summary>
        /// <param name="postOperatorDto">Модель Dto поштового оператора</param>
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
        /// Повертає екземпляр поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Екземпляр поштового оператора</returns>
        public PostOperatorDto GetById(int postOperatorId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperator, PostOperatorDto>()).CreateMapper();

            return mapper.Map<PostOperatorDto>(postOperatorsRepository.GetById(postOperatorId));
        }

        /// <summary>
        /// Повертає список усіх реалізованих в системі Delivery поштових операторів
        /// </summary>
        /// <returns>Список реалізованих операторів</returns>
        public IEnumerable<PostOperatorDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PostOperator, PostOperatorDto>()).CreateMapper();

            return mapper.Map<List<PostOperator>, List<PostOperatorDto>>((List<PostOperator>)postOperatorsRepository.GetAll());
        }

        /// <summary>
        /// Оновлює дані поштового оператора
        /// </summary>
        /// <param name="postOperatorDto">Екземпляр Dto поштового оператора</param>
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
