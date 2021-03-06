﻿using Delivery.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Shipments repo
    /// </summary>
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public InvoicesRepository(string connectionString) => this.connectionString = connectionString;

        /// <summary>
        /// Creates a new shipment
        /// </summary>
        /// <param name="invoice">Instance of the shipment</param>
        public void Create(IInvoice invoice)
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
                    CommandText = "dbo.sp_Invoices_Insert",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@AccountUserId", invoice.AccountUserId);
                cmd.Parameters.AddWithValue("@PostOperatorId", invoice.PostOperatorId);
                cmd.Parameters.AddWithValue("@Number", invoice.Number);
                cmd.Parameters.AddWithValue("@SendDateTime", invoice.SendDateTime);
                cmd.Parameters.AddWithValue("@Sender", invoice.Sender);
                cmd.Parameters.AddWithValue("@SenderAddress", invoice.SenderAddress);
                cmd.Parameters.AddWithValue("@Recipient", invoice.Recipient);
                cmd.Parameters.AddWithValue("@RecipientAddress", invoice.RecipientAddress);
                cmd.Parameters.AddWithValue("@CurrentLocation", invoice.CurrentLocation);
                cmd.Parameters.AddWithValue("@ActualStatus", invoice.ActualStatus);
                cmd.Parameters.AddWithValue("@Notes", invoice.Notes);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Помилка створення відправлення в базі даних." + ex.Message);
                }
            }
        }

        /// <summary>
        /// Returns the list of all shipments
        /// </summary>
        /// <returns>The list of shipments</returns>
        public IEnumerable<IInvoice> GetAll()
        {
            List<Invoice> listOfInvoices = new List<Invoice>();
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
                    CommandText = "dbo.sp_Invoices_Select",
                    CommandType = CommandType.StoredProcedure,
                };

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOfInvoices.Add(new Invoice
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                AccountUserId = reader["AccountUserId"].ToString(),
                                PostOperatorId = int.Parse(reader["PostOperatorId"].ToString()),
                                Number = reader["Number"].ToString(),
                                SendDateTime = DateTime.Parse(reader["SendDateTime"].ToString()),
                                Sender = reader["Sender"].ToString(),
                                SenderAddress = reader["SenderAddress"].ToString(),
                                Recipient = reader["Recipient"].ToString(),
                                RecipientAddress = reader["RecipientAddress"].ToString(),
                                CurrentLocation = reader["CurrentLocation"].ToString(),
                                ActualStatus = reader["ActualStatus"].ToString(),
                                Notes = reader["Notes"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання списку відправлень з бази даних.");
                }
            }
            return listOfInvoices;
        }

        /// <summary>
        /// Returns shipment by Id
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <returns>Instance of the shipment</returns>
        public IInvoice GetById(int invoiceId)
        {
            IInvoice invoice = new Invoice();
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
                    CommandText = "dbo.sp_Invoices_SelectById",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id", invoiceId);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        invoice.Id = invoiceId;
                        invoice.AccountUserId = reader["AccountUserId"].ToString();
                        invoice.PostOperatorId = int.Parse(reader["PostOperatorId"].ToString());
                        invoice.Number = reader["Number"].ToString();
                        invoice.SendDateTime = DateTime.Parse(reader["SendDateTime"].ToString());
                        invoice.Sender = reader["Sender"].ToString();
                        invoice.SenderAddress = reader["SenderAddress"].ToString();
                        invoice.Recipient = reader["Recipient"].ToString();
                        invoice.RecipientAddress = reader["RecipientAddress"].ToString();
                        invoice.CurrentLocation = reader["CurrentLocation"].ToString();
                        invoice.ActualStatus = reader["ActualStatus"].ToString();
                        invoice.Notes = reader["Notes"].ToString();
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання екземпляру відправлення з бази даних.");
                }
            }

            return invoice;

        }

        /// <summary>
        /// Returns user's shipments
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>The list of the user's shipments</returns>
        public IEnumerable<IInvoice> GetByUserId(string userId)
        {
            List<Invoice> listOfInvoices = new List<Invoice>();
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
                    CommandText = "dbo.sp_Invoices_SelectByUserId",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@AccountUserId", userId);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOfInvoices.Add(new Invoice
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                AccountUserId = reader["AccountUserId"].ToString(),
                                PostOperatorId = int.Parse(reader["PostOperatorId"].ToString()),
                                Number = reader["Number"].ToString(),
                                SendDateTime = DateTime.Parse(reader["SendDateTime"].ToString()),
                                Sender = reader["Sender"].ToString(),
                                SenderAddress = reader["SenderAddress"].ToString(),
                                Recipient = reader["Recipient"].ToString(),
                                RecipientAddress = reader["RecipientAddress"].ToString(),
                                CurrentLocation = reader["CurrentLocation"].ToString(),
                                ActualStatus = reader["ActualStatus"].ToString(),
                                Notes = reader["Notes"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання списку відправлень користувача з бази даних.");
                }
            }
            return listOfInvoices;

        }

        /// <summary>
        /// Returns dictionary: Id - Name of postal operators
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetPostOperatorsIdNames()
        {
            Dictionary<int, string> idNames = new Dictionary<int, string>();
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
                    CommandText = "dbo.sp_PostOperators_SelectIdNames",
                    CommandType = CommandType.StoredProcedure,
                };

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idNames.Add(int.Parse(reader["Id"].ToString()), reader["Name"].ToString());
                        }
                    }
                }
                catch (SqlException)
                {
                    throw new Exception("Помилка отримання списку IdNames поштових операторів з бази даних.");
                }
            }
            return idNames;

        }

        /// <summary>
        /// Deletes the user's shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        public void Delete(int invoiceId)
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
                    CommandText = "dbo.sp_Invoices_DeleteById",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id", invoiceId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Помилка видалення відправлення з бази даних." + ex.Message);
                }
            }
        }

        /// <summary>
        /// Deletes all shipments by user Id
        /// </summary>
        /// <param name="userId">User Id</param>
        public void DeleteByUserId(string userId)
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
                    CommandText = "dbo.sp_Invoices_DeleteByUserId",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@AccountUserId", userId);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Помилка видалення відправлень користувача з бази даних." + ex.Message);
                }
            }
        }

        /// <summary>
        /// Updates the current shipment status
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <param name="actualStatus">Current status</param>
        public void UpdateStatus(int invoiceId, string actualStatus)
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
                    CommandText = "dbo.sp_Invoices_UpdateActualStatusById",
                    CommandType = CommandType.StoredProcedure,
                };

                cmd.Parameters.AddWithValue("@Id", invoiceId);
                cmd.Parameters.AddWithValue("@ActualStatus", actualStatus);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Помилка оновлення статусу відправлення в базі даних." + ex.Message);
                }
            }

        }
    }
}
