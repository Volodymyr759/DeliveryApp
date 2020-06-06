using Delivery.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Репозиторій поштових операторів
    /// </summary>
    public class PostOperatorsRepository : IPostOperatorsRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Строка підключення</param>
        public PostOperatorsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Створює нового поштового оператора (при відповідній програмній реалізації)
        /// </summary>
        /// <param name="postOperator">Екземпляр поштового оператора</param>
        public void Create(IPostOperator postOperator)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException)
                {
                    throw new Exception("Немає підключення до бази даних.");
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "dbo.sp_PostOperators_Insert",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Name", postOperator.Name);
                cmd.Parameters.AddWithValue("@LinkToSearchPage", postOperator.LinkToSearchPage);
                cmd.Parameters.AddWithValue("@PathToLogoImage", postOperator.PathToLogoImage);
                cmd.Parameters.AddWithValue("@IsActive", postOperator.IsActive);
                cmd.Parameters.AddWithValue("@Notes", postOperator.Notes);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Помилка створення поштового оператора в базі даних." + ex.Message);
                }
            }
        }

        /// <summary>
        /// Повертає реалізовані на сервісі Delivery поштові оператори
        /// </summary>
        /// <returns>Список поштових операторів</returns>
        public IEnumerable<IPostOperator> GetAll()
        {
            List<PostOperator> listOfPostOperators = new List<PostOperator>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException)
                {
                    throw new Exception("Немає підключення до бази даних.");
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "dbo.sp_PostOperators_Select",
                    CommandType = CommandType.StoredProcedure,
                };

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOfPostOperators.Add(new PostOperator
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                LinkToSearchPage = reader["LinkToSearchPage"].ToString(),
                                PathToLogoImage = reader["PathToLogoImage"].ToString(),
                                IsActive = bool.Parse(reader["IsActive"].ToString()),
                                Notes = reader["Notes"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання списку поштових операторів з бази даних.");
                }
            }
            return listOfPostOperators;
        }

        /// <summary>
        /// Повертає екземпляр поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Eкземпляр поштового оператора</returns>
        public IPostOperator GetById(int postOperatorId)
        {
            IPostOperator postOperator = new PostOperator();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException)
                {
                    throw new Exception("Немає підключення до бази даних.");
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "dbo.sp_PostOperators_SelectById",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id", postOperatorId);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        postOperator.Id = postOperatorId;
                        postOperator.Name = reader["Name"].ToString();
                        postOperator.LinkToSearchPage = reader["LinkToSearchPage"].ToString();
                        postOperator.PathToLogoImage = reader["PathToLogoImage"].ToString();
                        postOperator.IsActive = bool.Parse(reader["IsActive"].ToString());
                        postOperator.Notes = reader["Notes"].ToString();

                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання екземпляру поштового оператора з бази даних.");
                }
            }

            return postOperator;
        }

        /// <summary>
        /// Оновлює запис поштового оператора
        /// </summary>
        /// <param name="postOperator">Екземпляр поштового оператора</param>
        public void Update(IPostOperator postOperator)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException)
                {
                    throw new Exception("Немає підключення до бази даних.");
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "dbo.sp_PostOperators_Update",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id", postOperator.Id);
                cmd.Parameters.AddWithValue("@Name", postOperator.Name);
                cmd.Parameters.AddWithValue("@LinkToSearchPage", postOperator.LinkToSearchPage);
                cmd.Parameters.AddWithValue("@PathToLogoImage", postOperator.PathToLogoImage);
                cmd.Parameters.AddWithValue("@IsActive", postOperator.IsActive);
                cmd.Parameters.AddWithValue("@Notes", postOperator.Notes);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка оновлення поштового оператора в базі даних.");
                }
            }
        }
    }
}
