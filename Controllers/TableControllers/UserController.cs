using System;
using System.Collections.Generic;
using System.Data;
using Rivel.DB;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers {
    public class UsersController : IController<User> {

        private readonly Database _db = Database.Instance;

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of User objects.</returns>
        public List<User> GetAll() {
            List<User> users = new List<User>();
            string query = "SELECT * FROM users";
            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows) {
                User user = new User(
                    userId: Convert.ToInt32(row["user_id"]),
                    username: row["username"].ToString(),
                    password: row["password"].ToString(),
                    roleId: Convert.ToInt32(row["role_id"])
                );
                users.Add(user);
            }
            return users;
        }

        /// <summary>
        /// Retrieves a user from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public User GetById(int id) {
            string query = $"SELECT * FROM users WHERE user_id = {id}";
            DataTable result = _db.ExecuteQuery(query);
            if (result.Rows.Count == 0) {
                throw new Exception("User not found.");
            }
            DataRow row = result.Rows[0];
            return new User(
                userId: Convert.ToInt32(row["user_id"]),
                username: row["username"].ToString(),
                password: row["password"].ToString(),
                roleId: Convert.ToInt32(row["role_id"])
            );
        }


        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user object to be created.</param>
        /// <returns>True if the user was created successfully, false otherwise.</returns>
        public bool Create(User user) {
            string query = $"INSERT INTO users (username, password, role_id) VALUES ('{user.Username}', '{user.Password}', {user.RoleId})";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool Update(int id, User user) {
            string query = $"UPDATE users SET username = '{user.Username}', password = '{user.Password}', role_id = {user.RoleId} WHERE user_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Deletes a user from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>True if the user was successfully deleted, false otherwise.</returns>
        public bool Delete(int id) {
            string query = $"DELETE FROM users WHERE user_id = {id}";
            int affectedRows = _db.ExecuteNonQuery(query);
            return affectedRows > 0;
        }

        /// <summary>
        /// Finds a user in the database based on their username and password.
        /// </summary>
        /// <param name="user">The user to search for.</param>
        /// <returns>True if the user is found, false otherwise.</returns>
        public bool Find(User user, out User foundUser) {
            string query = $"SELECT * FROM users WHERE username = '{user.Username}' AND password = '{user.Password}'";
            DataTable result = _db.ExecuteQuery(query);

            foundUser = new User(
                               userId: Convert.ToInt32(result.Rows[0]["user_id"]),
                                              username: result.Rows[0]["username"].ToString(),
                                                             password: result.Rows[0]["password"].ToString(),
                                                                            roleId: Convert.ToInt32(result.Rows[0]["role_id"])
                                                                                       );
            if (result.Rows.Count == 0)
                return false;
            return true;
        }
    }
}
