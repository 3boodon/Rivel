using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rivel.Services
{
    /// <summary>
    /// Provides methods for reading from and writing to files.
    /// </summary>
    /// <typeparam name="T">The type of data to read from or write to the file.</typeparam>
    internal class FileService<T>
    {
        /// <summary>
        /// Reads a list of objects of type T from a JSON file.
        /// </summary>
        /// <param name="relativeFilePath">The relative path of the file to read from.</param>
        /// <returns>A list of objects of type T.</returns>
        public List<T> ReadFromFile(string relativeFilePath)
        {
            string filePath = AdjustFilePath(relativeFilePath);
            EnsureDirectoryExists(filePath);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            return new List<T>();
        }

        /// <summary>
        /// Saves a list of items to a file in JSON format.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="items">The list of items to save.</param>
        /// <param name="filePath">The path of the file to save to.</param>
        public void SaveToFile(List<T> items, string filePath)
        {
            try
            {
                // Check if directory exists, if not, create it
                EnsureDirectoryExists(filePath);

                // Serialize and save
                string json = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Saved to file: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Adjusts the given relative file path to an absolute file path.
        /// </summary>
        /// <param name="relativeFilePath">The relative file path to adjust.</param>
        /// <returns>The adjusted absolute file path.</returns>
        private string AdjustFilePath(string relativeFilePath)
        {

            //return Path.Combine(@"..\..\..\", relativeFilePath);
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath));
            //return relativeFilePath;
        }

        /// <summary>
        /// Ensures that the directory of the specified file path exists. If it does not exist, creates the directory.
        /// </summary>
        /// <param name="filePath">The file path to check.</param>
        private void EnsureDirectoryExists(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine($"Created directory: {directory}");
            }
        }
    }
}
