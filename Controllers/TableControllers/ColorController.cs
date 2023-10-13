using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers
{
    /// <summary>
    /// Controller for managing colors in the database.
    /// </summary>
    public class ColorController : IController<Color>
    {
        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Gets all colors from the database.
        /// </summary>
        /// <returns>A list of all colors in the database.</returns>
        public List<Color> GetAll()
        {
            List<Color> colors = new List<Color>();
            string query = "SELECT * FROM colors";
            DataTable dt = _db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Color color = new Color(
                    colorId: Convert.ToInt32(row["color_id"]),
                    colorName: row["color_name"].ToString()
                );
                colors.Add(color);
            }
            return colors;
        }

        /// <summary>
        /// Gets a color by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the color to get.</param>
        /// <returns>The color with the specified ID, or null if no color was found.</returns>
        public Color GetById(int id)
        {
            string query = $"SELECT * FROM colors WHERE color_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = result.Rows[0];
            return new Color(
                colorId: Convert.ToInt32(row["color_id"]),
                colorName: row["color_name"].ToString()
            );
        }

        /// <summary>
        /// Creates a new color in the database.
        /// </summary>
        /// <param name="color">The color to create.</param>
        /// <returns>True if the color was created successfully, false otherwise.</returns>
        public bool Create(Color color)
        {
            string query = $"INSERT INTO colors (color_name) VALUES ('{color.ColorName}')";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing color in the database.
        /// </summary>
        /// <param name="id">The ID of the color to update.</param>
        /// <param name="color">The updated color data.</param>
        /// <returns>True if the color was updated successfully, false otherwise.</returns>
        public bool Update(int id, Color color)
        {
            string query = $"UPDATE colors SET color_name = '{color.ColorName}' WHERE color_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a color from the database.
        /// </summary>
        /// <param name="id">The ID of the color to delete.</param>
        /// <returns>True if the color was deleted successfully, false otherwise.</returns>
        public bool Delete(int id)
        {
            string query = $"DELETE FROM colors WHERE color_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a color by its ID in the database.
        /// </summary>
        /// <param name="id">The ID of the color to find.</param>
        /// <returns>The color with the specified ID, or null if no color was found.</returns>
        public Color Find(int id)
        {
            return GetById(id);
        }
    }
}
