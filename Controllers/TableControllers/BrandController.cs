using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller class for managing Brand objects in the database.
    /// Implements the IController interface.
    /// </summary>
    public class BrandController : IController<Brand>
    {
        private readonly Database _db = Database.Instance;

        public BrandController()
        {
            _db = Database.Instance;
        }

        /// <summary>
        /// Retrieves all Brand objects from the database.
        /// </summary>
        /// <returns>A list of all Brand objects in the database.</returns>
        public List<Brand> GetAll()
        {
            List<Brand> brands = new List<Brand>();
            string query = "SELECT * FROM brands";
            DataTable dt = _db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Brand brand = new Brand(
                    brandId: Convert.ToInt32(row["brand_id"]),
                    brandName: row["brand_name"].ToString()
                );
                brands.Add(brand);
            }
            return brands;
        }

        /// <summary>
        /// Retrieves a single Brand object from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Brand object to retrieve.</param>
        /// <returns>The Brand object with the specified ID, or null if not found.</returns>
        public Brand GetById(int id)
        {
            string query = $"SELECT * FROM brands WHERE brand_id = {id}";
            DataTable dt = _db.ExecuteQuery(query);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            return new Brand(
                brandName: row["brand_name"].ToString()
            );
        }

        /// <summary>
        /// Finds a single Brand object from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Brand object to find.</param>
        /// <returns>The Brand object with the specified ID, or null if not found.</returns>
        public Brand Find(int id)
        {
            return GetById(id);
        }

        /// <summary>
        /// Updates a Brand object in the database.
        /// </summary>
        /// <param name="id">The ID of the Brand object to update.</param>
        /// <param name="brand">The updated Brand object.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool Update(int id, Brand brand)
        {
            string query = $"UPDATE brands SET brand_name = '{brand.BrandName}' WHERE brand_id = {id}";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Deletes a Brand object from the database.
        /// </summary>
        /// <param name="id">The ID of the Brand object to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM brands WHERE brand_id = {id}";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Creates a new Brand object in the database.
        /// </summary>
        /// <param name="brand">The Brand object to create.</param>
        /// <returns>True if the creation was successful, false otherwise.</returns>
        public bool Create(Brand brand)
        {
            string query = $"INSERT INTO brands (brand_name) VALUES ('{brand.BrandName}')";
            int rowsAffected = _db.ExecuteNonQuery(query);
            return rowsAffected > 0;
        }
    }
}
