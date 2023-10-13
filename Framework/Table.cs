using System;
using System.Collections.Generic;
using System.Reflection;
using Rivel.DB;

namespace Rivel.Framework
{
    internal class Table<T>
    {

        private static readonly Database _db = Database.Instance;
        /// <summary>
        /// Gets the list of columns for the table.
        /// </summary>
        private static List<Column> Columns
        {
            get
            {
                List<Column> columns = new List<Column>();
                // loop through props to get the List<Column> columns
                foreach (var prop in typeof(T).GetProperties())
                {
                    columns.Add(new Column(prop.Name));
                }
                Console.Write("\n");
                return columns;
            }
        }

        /// <summary>
        /// Renders a table of objects with the specified table width and table name.
        /// </summary>
        /// <typeparam name="T">The type of objects to render.</typeparam>
        /// <param name="objects">The list of objects to render.</param>
        /// <param name="tableWidth">The width of the table as an int value</param>
        /// <param name="tableName">The name of the table which matches the name in your database</param>
        public static void Render(List<T> objects, int tableWidth, string tableName)
        {

            // Header
            RenderHeader(tableName, Columns, tableWidth);

            // Body
            RenderBody(objects, tableName, Columns, tableWidth);

        }


        /// <summary>
        /// Renders the body of the table by printing all properties of each object in the list as a row.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="objects">The list of objects to print.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">The list of columns in the table.</param>
        /// <param name="tableWidth">The width of the table.</param>
        private static void RenderBody(List<T> objects, string tableName, List<Column> columns, int tableWidth)
        {
            foreach (T obj in objects)
            {
                // print all properties as a row
                foreach (var prop in obj.GetType().GetProperties())
                {

                    // get the biggest length of a value using a query
                    HeaderLength(tableName, columns, obj, prop, out int index, out string value);

                    Helper.WriteIn(ConsoleColor.DarkGray, $"{value}{Helper.Repeat('\t', columns[index].Spaces)}");
                }
                Console.Write("\n");
            }
            Helper.WriteLineIn(ConsoleColor.DarkGray, Helper.Repeat('-', tableWidth));

        }

        /// <summary>
        /// Calculates the length of the header for a given column and returns the value of the property as a string with padding if necessary.
        /// </summary>
        /// <param name="tableName">The name of the table in the database.</param>
        /// <param name="columns">The list of columns in the table.</param>
        /// <param name="obj">The object containing the property value.</param>
        /// <param name="prop">The property to get the value from.</param>
        /// <param name="index">The index of the column in the list.</param>
        /// <param name="value">The value of the property as a string with padding if necessary.</param>
        private static void HeaderLength(string tableName, List<Column> columns, T obj, PropertyInfo prop, out int index, out string value)
        {
            // get the index of the column in the list
            index = columns.FindIndex(column => column.Name == prop.Name);

            // get the biggest length of a value using a query
            string columnName = (prop.Name != "CreatedDate" && prop.Name != "UpdatedDate") ? prop.Name.ToSnakeCase() : prop.Name;
            object maxLength =
                _db.ExecuteScalar($"SELECT MAX(LENGTH({columnName})) FROM {tableName}");
            int headerLength = $"{prop.Name}{Helper.Repeat('\t', columns[index].Spaces)}".Length;

            // get the value of the property as a string with padding if necessary
            if (Convert.ToInt32(maxLength) > headerLength)
            {
                value = prop.GetValue(obj).ToString().Fill(Convert.ToInt32(maxLength));
            }
            else
            {
                value = prop.GetValue(obj).ToString().Fill(Convert.ToInt32(headerLength));
            }
        }

        /// <summary>
        /// Renders the header of a table with the given table name, columns, and table width.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="columns">The list of columns in the table.</param>
        /// <param name="tableWidth">The width of the table.</param>
        private static void RenderHeader(string tableName, List<Column> columns, int tableWidth)
        {
            foreach (Column column in columns)
            {
                string value = GetColumnName(tableName, column);
                Helper.WriteIn(ConsoleColor.Yellow, $"{value}{Helper.Repeat('\t', column.Spaces)}");
            }
            Console.Write("\n");
            Helper.WriteLineIn(ConsoleColor.DarkGray, Helper.Repeat('-', tableWidth));
        }

        /// <summary>
        /// Gets the name of the column in the specified table, formatted for display in a table header.
        /// </summary>
        /// <param name="tableName">The name of the table containing the column.</param>
        /// <param name="column">The column to get the name for.</param>
        /// <returns>The formatted column name.</returns>
        private static string GetColumnName(string tableName, Column column)
        {
            string columnName = (column.Name != "CreatedDate" && column.Name != "UpdatedDate") ? column.Name.ToSnakeCase() : column.Name;
            object maxLength =
                _db.ExecuteScalar($"SELECT MAX(LENGTH({columnName})) FROM {tableName}");

            int headerLength = $"{column.Name}{Helper.Repeat('\t', column.Spaces)}".Length;
            string value = Convert.ToInt32(maxLength) > headerLength ? column.Name.Fill(Convert.ToInt32(maxLength)) : column.Name.Fill(headerLength);
            return value;
        }
    }


    /// <summary>
    /// Represents a column in a table.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to use for padding the column.
        /// </summary>
        public int Spaces { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with the specified name and number of spaces.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="spaces">The number of spaces to use for padding the column. Defaults to 1.</param>
        public Column(string name, int spaces = 1)
        {
            Name = name;
            Spaces = spaces;
        }
    }
}
