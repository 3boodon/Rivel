using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Rivel.DB
{
    /// <summary>
    /// Represents a singleton database connection to a MySQL database.
    /// </summary>
    public class Database
    {
        private static Database _instance;
        private static MySqlConnection _connection;
        private static readonly string _connectionString = "Server=localhost;Database=rivel;User ID=root;Password=root;";

        // Private constructor to prevent instantiation from other classes
        private Database()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
        }

        /// <summary>
        /// Gets the singleton instance of the Database class.
        /// </summary>
        /// <returns>The singleton instance.</returns>
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Executes a query that returns data (e.g., SELECT).
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>A DataTable containing the results.</returns>
        public DataTable ExecuteQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// Executes a query that does not return any data (e.g., INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>The number of affected rows.</returns>
        public int ExecuteNonQuery(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    // Throw exception for unknown IDs
                    throw new Exception("No records were affected. Unknown ID?");
                }
                return affectedRows;
            }
            catch (MySqlException ex)
            {
                // Throw exception for foreign key constraint errors
                if (ex.Number == 1451)
                {
                    throw new Exception("Cannot delete or update a parent row: a foreign key constraint fails.");
                }
                else
                {
                    // Throw other MySQL exceptions
                    throw new Exception($"An MySQL error occurred: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // Throw general exceptions
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        public object ExecuteScalar(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            return cmd.ExecuteScalar();
        }


        /// <summary>
        /// Closes the existing database connection.
        /// </summary>
        /// <returns>True if the connection is closed successfully, otherwise false.</returns>
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
