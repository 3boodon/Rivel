using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers {
    /// <summary>
    /// Controller class for handling CRUD operations for the Car entity.
    /// </summary>
    public class CarController : IController<Car> {
        private readonly Database _db = Database.Instance;


        /// <summary>
        /// Retrieves all Car entities from the database.
        /// </summary>
        /// <returns>A list of Car entities.</returns>
        public List<Car> GetAll() {
            List<Car> cars = new List<Car>();
            string query = "SELECT * FROM cars";
            DataTable dt = _db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows) {
                Car car = new Car {
                    CarId = Convert.ToInt32(row["car_id"]),
                    BrandId = Convert.ToInt32(row["brand_id"]),
                    ModelId = Convert.ToInt32(row["model_id"]),
                    ColorId = Convert.ToInt32(row["color_id"]),
                    CarStatusId = Convert.ToInt32(row["car_status_id"]),
                    PricePerDay = Convert.ToDecimal(row["price_per_day"])
                };
                cars.Add(car);
            }
            return cars;
        }

        /// <summary>
        /// Retrieves a specific Car entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Car entity to retrieve.</param>
        /// <returns>The Car entity with the specified ID, or null if it does not exist.</returns>
        public Car GetById(int id) {
            string query = $"SELECT * FROM cars WHERE car_id = {id}";
            DataTable dt = _db.ExecuteQuery(query);

            if (dt.Rows.Count == 0) {
                return null;
            }

            DataRow row = dt.Rows[0];
            Car car = new Car {
                CarId = Convert.ToInt32(row["car_id"]),
                BrandId = Convert.ToInt32(row["brand_id"]),
                ModelId = Convert.ToInt32(row["model_id"]),
                ColorId = Convert.ToInt32(row["color_id"]),
                CarStatusId = Convert.ToInt32(row["car_status_id"]),
                PricePerDay = Convert.ToDecimal(row["price_per_day"])
            };
            return car;
        }

        /// <summary>
        /// Checks if a specific Car entity exists in the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Car entity to check.</param>
        /// <returns>True if the Car entity exists, false otherwise.</returns>
        public bool Find(int id) {
            return GetById(id) != null;
        }

        /// <summary>
        /// Updates a specific Car entity in the database.
        /// </summary>
        /// <param name="id">The ID of the Car entity to update.</param>
        /// <param name="entity">The updated Car entity.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool Update(int id, Car entity) {
            string query = $"UPDATE cars SET brand_id = {entity.BrandId}, model_id = {entity.ModelId}, color_id = {entity.ColorId}, car_status_id = {entity.CarStatusId}, price_per_day = {entity.PricePerDay} WHERE car_id = {id}";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific Car entity from the database.
        /// </summary>
        /// <param name="id">The ID of the Car entity to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public bool Delete(int id) {
            string query = $"DELETE FROM cars WHERE car_id = {id}";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Creates a new Car entity in the database.
        /// </summary>
        /// <param name="entity">The Car entity to create.</param>
        /// <returns>True if the creation was successful, false otherwise.</returns>
        public bool Create(Car entity) {
            string query = $"INSERT INTO cars (brand_id, model_id, color_id, car_status_id, price_per_day) VALUES ({entity.BrandId}, {entity.ModelId}, {entity.ColorId}, {entity.CarStatusId}, {entity.PricePerDay})";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }
    }
}
