using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller for managing car statuses in the database.
    /// </summary>
    public class CarStatusController : IController<CarStatus>
    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Retrieves all car statuses from the database.
        /// </summary>
        /// <returns>A list of all car statuses.</returns>
        public List<CarStatus> GetAll()
        {
            List<CarStatus> carStatuses = new List<CarStatus>();
            string query = "SELECT * FROM car_statuses";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                CarStatus carStatus = new CarStatus(
                    statusId: Convert.ToInt32(row["car_status_id"]),
                    statusName: row["car_status_name"].ToString()
                );
                carStatuses.Add(carStatus);
            }
            return carStatuses;
        }

        /// <summary>
        /// Retrieves a specific car status from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the car status to retrieve.</param>
        /// <returns>The car status with the specified ID.</returns>
        /// <exception cref="Exception">Thrown when the car status with the specified ID is not found.</exception>
        public CarStatus GetById(int id)
        {
            string query = $"SELECT * FROM car_statuses WHERE car_status_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                throw new Exception("Car status not found.");
            }
            DataRow row = result.Rows[0];
            return new CarStatus(
                statusName: row["car_status_name"].ToString()
            );
        }

        /// <summary>
        /// Creates a new car status in the database.
        /// </summary>
        /// <param name="carStatus">The car status to create.</param>
        /// <returns>True if the car status was created successfully, false otherwise.</returns>
        public bool Create(CarStatus carStatus)
        {
            string query = $"INSERT INTO car_statuses (car_status_name) VALUES ('{carStatus.CarStatusName}')";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing car status in the database.
        /// </summary>
        /// <param name="id">The ID of the car status to update.</param>
        /// <param name="carStatus">The updated car status.</param>
        /// <returns>True if the car status was updated successfully, false otherwise.</returns>
        public bool Update(int id, CarStatus carStatus)
        {
            string query = $"UPDATE car_statuses SET car_status_name = '{carStatus.CarStatusName}' WHERE car_status_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a car status from the database.
        /// </summary>
        /// <param name="id">The ID of the car status to delete.</param>
        /// <returns>True if the car status was deleted successfully, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM car_statuses WHERE car_status_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a specific car status in the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the car status to find.</param>
        /// <returns>The car status with the specified ID.</returns>
        public CarStatus Find(int id)
        {
            return GetById(id);
        }
    }
}
