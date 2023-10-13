using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller class for PaymentMethod model. Implements IController interface.
    /// </summary>
    public class PaymentMethodController : IController<PaymentMethod>
    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Gets all payment methods from the database.
        /// </summary>
        /// <returns>A list of PaymentMethod objects.</returns>
        public List<PaymentMethod> GetAll()
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            string query = "SELECT * FROM payment_methods";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                PaymentMethod paymentMethod = new PaymentMethod(
                    paymentMethodId: Convert.ToInt32(row["payment_method_id"]),
                    paymentMethodName: row["payment_method_name"].ToString()
                );
                paymentMethods.Add(paymentMethod);
            }
            return paymentMethods;
        }

        /// <summary>
        /// Gets a payment method by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the payment method to retrieve.</param>
        /// <returns>A PaymentMethod object.</returns>
        /// <exception cref="Exception">Thrown when the payment method is not found.</exception>
        public PaymentMethod GetById(int id)
        {
            string query = $"SELECT * FROM payment_methods WHERE payment_method_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                throw new Exception("Payment method not found.");
            }
            DataRow row = result.Rows[0];
            return new PaymentMethod(
                paymentMethodName: row["payment_method_name"].ToString()
            );
        }

        /// <summary>
        /// Creates a new payment method in the database.
        /// </summary>
        /// <param name="paymentMethod">The PaymentMethod object to create.</param>
        /// <returns>True if the payment method was created successfully, false otherwise.</returns>
        public bool Create(PaymentMethod paymentMethod)
        {
            string query = $"INSERT INTO payment_methods (payment_method_name) VALUES ('{paymentMethod.PaymentMethodName}')";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing payment method in the database.
        /// </summary>
        /// <param name="id">The ID of the payment method to update.</param>
        /// <param name="paymentMethod">The updated PaymentMethod object.</param>
        /// <returns>True if the payment method was updated successfully, false otherwise.</returns>
        public bool Update(int id, PaymentMethod paymentMethod)
        {
            string query = $"UPDATE payment_methods SET payment_method_name = '{paymentMethod.PaymentMethodName}' WHERE payment_method_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a payment method from the database.
        /// </summary>
        /// <param name="id">The ID of the payment method to delete.</param>
        /// <returns>True if the payment method was deleted successfully, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM payment_methods WHERE payment_method_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a payment method by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the payment method to retrieve.</param>
        /// <returns>A PaymentMethod object.</returns>
        public PaymentMethod Find(int id)
        {
            return GetById(id);
        }
    }
}
