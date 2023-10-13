using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on Rental objects in the database.
    /// </summary>
    public class RentalController : IController<Rental>
    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Retrieves all Rental objects from the database.
        /// </summary>
        /// <returns>A list of all Rental objects in the database.</returns>
        public List<Rental> GetAll()
        {
            List<Rental> rentals = new List<Rental>();
            string query = "SELECT * FROM rentals";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                Rental rental = new Rental(
                    rentalId: Convert.ToInt32(row["rental_id"]),
                    warrantyId: Convert.ToInt32(row["warranty_id"]),
                    userId: Convert.ToInt32(row["user_id"]),
                    carId: Convert.ToInt32(row["car_id"]),
                    pickupDate: Convert.ToDateTime(row["pickup_date"]),
                    returnDate: Convert.ToDateTime(row["return_date"]),
                    paymentId: Convert.ToInt32(row["payment_id"])
                );
                rentals.Add(rental);
            }
            return rentals;
        }

        /// <summary>
        /// Retrieves a single Rental object from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Rental object to retrieve.</param>
        /// <returns>The Rental object with the specified ID.</returns>
        /// <exception cref="Exception">Thrown when no Rental object with the specified ID is found.</exception>
        public Rental GetById(int id)
        {
            string query = $"SELECT * FROM rentals WHERE rental_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                throw new Exception("Rental not found.");
            }
            DataRow row = result.Rows[0];
            return new Rental(
                warrantyId: Convert.ToInt32(row["warranty_id"]),
                userId: Convert.ToInt32(row["user_id"]),
                carId: Convert.ToInt32(row["car_id"]),
                pickupDate: Convert.ToDateTime(row["pickup_date"]),
                returnDate: Convert.ToDateTime(row["return_date"]),
                paymentId: Convert.ToInt32(row["payment_id"])
            );
        }

        /// <summary>
        /// Creates a new Rental object in the database.
        /// </summary>
        /// <param name="rental">The Rental object to create.</param>
        /// <returns>True if the Rental object was successfully created, false otherwise.</returns>
        public bool Create(Rental rental)
        {
            string query = $"INSERT INTO rentals (warranty_id, user_id, car_id, pickup_date, return_date, payment_id) VALUES ({rental.WarrantyId}, {rental.UserId}, {rental.CarId}, '{rental.PickupDate}', '{rental.ReturnDate}', {rental.PaymentId})";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing Rental object in the database.
        /// </summary>
        /// <param name="id">The ID of the Rental object to update.</param>
        /// <param name="rental">The updated Rental object.</param>
        /// <returns>True if the Rental object was successfully updated, false otherwise.</returns>
        public bool Update(int id, Rental rental)
        {
            string query = $"UPDATE rentals SET warranty_id = {rental.WarrantyId}, user_id = {rental.UserId}, car_id = {rental.CarId}, pickup_date = '{rental.PickupDate}', return_date = '{rental.ReturnDate}', payment_id = {rental.PaymentId} WHERE rental_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a Rental object from the database.
        /// </summary>
        /// <param name="id">The ID of the Rental object to delete.</param>
        /// <returns>True if the Rental object was successfully deleted, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM rentals WHERE rental_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Checks if a Rental object already exists in the database.
        /// </summary>
        /// <param name="rental">The Rental object to check for.</param>
        /// <returns>True if a Rental object with the same user ID, car ID, pickup date, and return date already exists in the database, false otherwise.</returns>
        public bool Find(Rental rental)
        {
            string query = $"SELECT * FROM rentals WHERE user_id = {rental.UserId} AND car_id = {rental.CarId} AND pickup_date = '{rental.PickupDate}' AND return_date = '{rental.ReturnDate}'";
            DataTable result = _db.ExecuteQuery(query);

            if (result.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
