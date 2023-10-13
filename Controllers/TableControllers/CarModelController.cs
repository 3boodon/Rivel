using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller for managing car models in the database.
    /// </summary>
    public class CarModelController : IController<CarModel>
    {
        private readonly Database _database;

        public CarModelController()
        {
            _database = Database.Instance;
        }

        /// <summary>
        /// Retrieves a list of all car models from the database.
        /// </summary>
        /// <returns>A list of CarModel objects.</returns>
        public List<CarModel> GetAll()
        {
            List<CarModel> carModels = new List<CarModel>();
            string query = "SELECT * FROM models";
            DataTable dt = _database.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                CarModel carModel = new CarModel(modelId: Convert.ToInt32(row["model_id"]), modelName: row["model_name"].ToString(), brandId: Convert.ToInt32(row["brand_id"]));
                carModels.Add(carModel);
            }
            return carModels;
        }

        /// <summary>
        /// Retrieves a specific car model from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the car model to retrieve.</param>
        /// <returns>A CarModel object representing the retrieved car model, or null if no car model was found with the specified ID.</returns>
        public CarModel GetById(int id)
        {
            string query = $"SELECT * FROM models WHERE model_id = {id}";
            DataTable dt = _database.ExecuteQuery(query);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            return new CarModel(modelName: row["model_name"].ToString(), brandId: Convert.ToInt32(row["brand_id"]));
        }

        /// <summary>
        /// Finds a specific car model from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the car model to find.</param>
        /// <returns>A CarModel object representing the found car model, or null if no car model was found with the specified ID.</returns>
        public CarModel Find(int id)
        {
            return GetById(id);
        }

        /// <summary>
        /// Updates a specific car model in the database.
        /// </summary>
        /// <param name="id">The ID of the car model to update.</param>
        /// <param name="carModel">A CarModel object containing the updated information for the car model.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool Update(int id, CarModel carModel)
        {
            string query = $"UPDATE models SET model_name = '{carModel.ModelName}', brand_id = {carModel.BrandId} WHERE model_id = {id}";
            int rowsAffected = _database.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a specific car model from the database.
        /// </summary>
        /// <param name="id">The ID of the car model to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM models WHERE model_id = {id}";
            int rowsAffected = _database.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Creates a new car model in the database.
        /// </summary>
        /// <param name="carModel">A CarModel object containing the information for the new car model.</param>
        /// <returns>True if the creation was successful, false otherwise.</returns>
        public bool Create(CarModel carModel)
        {
            string query = $"INSERT INTO models (model_name, brand_id) VALUES ('{carModel.ModelName}', {carModel.BrandId})";
            int rowsAffected = _database.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }
    }
}
