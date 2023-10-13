using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller for managing payments in the database.
    /// </summary>
    public class PaymentController : IController<Payment>
    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Retrieves all payments from the database.
        /// </summary>
        /// <returns>A list of Payment objects.</returns>
        public List<Payment> GetAll()
        {
            List<Payment> payments = new List<Payment>();
            string query = "SELECT * FROM payments";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                Payment payment = new Payment(
                    paymentId: Convert.ToInt32(row["payment_id"]),
                    paymentMethodId: Convert.ToInt32(row["payment_method_id"]),
                    userId: Convert.ToInt32(row["user_id"]),
                    paymentAmount: Convert.ToDecimal(row["payment_amount"])
                );
                payments.Add(payment);
            }
            return payments;
        }

        // get last payment
        public Payment GetLastPayment()
        {
            string query = "SELECT * FROM payments ORDER BY payment_id DESC LIMIT 1";
            DataTable dt = _db.ExecuteQuery(query);
            if (dt.Rows.Count == 0) throw new Exception("Payment not found.");

            DataRow row = dt.Rows[0];
            Payment payment = new Payment(
                paymentId: Convert.ToInt32(row["payment_id"]),
                paymentMethodId: Convert.ToInt32(row["payment_method_id"]),
                userId: Convert.ToInt32(row["user_id"]),
                paymentAmount: Convert.ToDecimal(row["payment_amount"])
            );
            return payment;
        }

        /// <summary>
        /// Retrieves a payment from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        /// <returns>A Payment object.</returns>
        /// <exception cref="Exception">Thrown when the payment is not found.</exception>
        public Payment GetById(int id)
        {
            string query = $"SELECT * FROM payments WHERE payment_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                throw new Exception("Payment not found.");
            }
            DataRow row = result.Rows[0];
            return new Payment(
                paymentMethodId: Convert.ToInt32(row["payment_method_id"]),
                userId: Convert.ToInt32(row["user_id"]),
                paymentAmount: Convert.ToDecimal(row["payment_amount"])
            );
        }

        /// <summary>
        /// Creates a new payment in the database.
        /// </summary>
        /// <param name="payment">The Payment object to create.</param>
        /// <returns>True if the payment was created successfully, false otherwise.</returns>
        public bool Create(Payment payment)
        {
            string query = $"INSERT INTO payments (payment_method_id, user_id, payment_amount) VALUES ({payment.PaymentMethodId}, {payment.UserId}, {payment.PaymentAmount})";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing payment in the database.
        /// </summary>
        /// <param name="id">The ID of the payment to update.</param>
        /// <param name="payment">The updated Payment object.</param>
        /// <returns>True if the payment was updated successfully, false otherwise.</returns>
        public bool Update(int id, Payment payment)
        {
            string query = $"UPDATE payments SET payment_method_id = {payment.PaymentMethodId}, user_id = {payment.UserId}, payment_amount = {payment.PaymentAmount} WHERE payment_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a payment from the database.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        /// <returns>True if the payment was deleted successfully, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM payments WHERE payment_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a payment in the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to find.</param>
        /// <returns>A Payment object.</returns>
        public Payment Find(int id)
        {
            return GetById(id);
        }
    }
}
