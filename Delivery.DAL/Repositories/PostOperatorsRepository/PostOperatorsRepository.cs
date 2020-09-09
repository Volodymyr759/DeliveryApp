using Delivery.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Postal operators repo
    /// </summary>
    public class PostOperatorsRepository : IPostOperatorsRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public PostOperatorsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Creates the new postal operator
        /// </summary>
        /// <param name="postOperator">Instance of the postal operator</param>
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
        /// Returns the list of postal operators
        /// </summary>
        /// <returns>The list of postal operators</returns>
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
        /// Returns postal operator by Id
        /// </summary>
        /// <param name="postOperatorId">PostOperator Id</param>
        /// <returns>Instance of the postal operator</returns>
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
        /// Updates postal operator
        /// </summary>
        /// <param name="postOperator">Instance of the postal operator</param>
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
