using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers {
    /// <summary>
    /// Controller class for managing reservations in the database.
    /// Implements the IController interface for CRUD operations.
    /// </summary>
    public class ReservationController : IController<Reservation> {

        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Retrieves all reservations from the database.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>
        public List<Reservation> GetAll() {
            List<Reservation> reservations = new List<Reservation>();
            string query = "SELECT * FROM reservations";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows) {
                Reservation reservation = new Reservation(
                    reservationId: Convert.ToInt32(row["reservation_id"]),
                    userId: Convert.ToInt32(row["user_id"]),
                    carId: Convert.ToInt32(row["car_id"]),
                    pickupDate: Convert.ToString(row["pickup_date"]),
                    returnDate: Convert.ToString(row["return_date"]),
                    paymentId: Convert.ToInt32(row["payment_id"])
                );
                reservations.Add(reservation);
            }
            return reservations;
        }

        /// <summary>
        /// Retrieves a reservation by its ID.
        /// </summary>
        /// <param name="id">The ID of the reservation to retrieve.</param>
        /// <returns>The reservation with the specified ID.</returns>
        public Reservation GetById(int id) {
            string query = $"SELECT * FROM reservations WHERE reservation_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0) {
                throw new Exception("Reservation not found.");
            }
            DataRow row = result.Rows[0];
            return new Reservation(
                userId: Convert.ToInt32(row["user_id"]),
                carId: Convert.ToInt32(row["car_id"]),
                pickupDate: Convert.ToString(row["pickup_date"]),
                returnDate: Convert.ToString(row["return_date"]),
                paymentId: Convert.ToInt32(row["payment_id"])
            );
        }

        /// <summary>
        /// Creates a new reservation in the database.
        /// </summary>
        /// <param name="reservation">The reservation object to be created.</param>
        /// <returns>True if the reservation was created successfully, false otherwise.</returns>
        public bool Create(Reservation reservation) {
            string query = $"INSERT INTO reservations (user_id, car_id, pickup_date, return_date, payment_id) VALUES ({reservation.UserId}, {reservation.CarId}, '{reservation.PickupDate}', '{reservation.ReturnDate}', {reservation.PaymentId})";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing reservation in the database.
        /// </summary>
        /// <param name="id">The ID of the reservation to update.</param>
        /// <param name="reservation">The updated reservation object.</param>
        /// <returns>True if the reservation was updated successfully, false otherwise.</returns>
        public bool Update(int id, Reservation reservation) {
            string query = $"UPDATE reservations SET user_id = {reservation.UserId}, car_id = {reservation.CarId}, pickup_date = '{reservation.PickupDate}', return_date = '{reservation.ReturnDate}', payment_id = {reservation.PaymentId} WHERE reservation_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a reservation from the database.
        /// </summary>
        /// <param name="id">The ID of the reservation to delete.</param>
        /// <returns>True if the reservation was successfully deleted, false otherwise.</returns>
        public bool Delete(int id) {
            string query = $"DELETE FROM reservations WHERE reservation_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Represents a reservation made by a customer.
        /// </summary>
        public Reservation Find(int id) {
            return GetById(id);
        }
    }
}
