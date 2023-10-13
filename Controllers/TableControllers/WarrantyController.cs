using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller class for managing warranties in the database.
    /// Implements the IController interface for CRUD operations.
    /// </summary>
    public class WarrantyController : IController<Warranty>

    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Gets all warranties from the database.
        /// </summary>
        /// <returns>A list of all warranties.</returns>
        public List<Warranty> GetAll()
        {
            List<Warranty> warranties = new List<Warranty>();
            string query = "SELECT * FROM warranties";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                Warranty warranty = new Warranty(
                    warrantyId: Convert.ToInt32(row["warranty_id"]),
                    warrantyName: row["warranty_name"].ToString()
                );
                warranties.Add(warranty);
            }
            return warranties;
        }

        /// <summary>
        /// Gets a warranty by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the warranty to get.</param>
        /// <returns>The warranty with the specified ID.</returns>
        public Warranty GetById(int id)
        {
            string query = $"SELECT * FROM warranties WHERE warranty_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                throw new Exception("Warranty not found.");
            }
            DataRow row = result.Rows[0];
            return new Warranty(
                warrantyName: row["warranty_name"].ToString()
            );
        }

        /// <summary>
        /// Creates a new warranty in the database.
        /// </summary>
        /// <param name="warranty">The warranty to create.</param>
        /// <returns>True if the warranty was created successfully, false otherwise.</returns>
        public bool Create(Warranty warranty)
        {
            string query = $"INSERT INTO warranties (warranty_name) VALUES ('{warranty.WarrantyName}')";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing warranty in the database.
        /// </summary>
        /// <param name="id">The ID of the warranty to update.</param>
        /// <param name="warranty">The updated warranty data.</param>
        /// <returns>True if the warranty was updated successfully, false otherwise.</returns>
        public bool Update(int id, Warranty warranty)
        {
            string query = $"UPDATE warranties SET warranty_name = '{warranty.WarrantyName}' WHERE warranty_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a warranty from the database.
        /// </summary>
        /// <param name="id">The ID of the warranty to delete.</param>
        /// <returns>True if the warranty was deleted successfully, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM warranties WHERE warranty_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a warranty by its name in the database.
        /// </summary>
        /// <param name="warranty">The warranty to find.</param>
        /// <returns>True if the warranty was found, false otherwise.</returns>
        public bool Find(Warranty warranty)
        {
            string query = $"SELECT * FROM warranties WHERE warranty_name = '{warranty.WarrantyName}'";
            DataTable result = _db.ExecuteQuery(query);

            if (result.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
