using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rivel.Services {
    internal class FileService<T> {
        public List<T> ReadFromFile(string relativeFilePath) {
            string filePath = AdjustFilePath(relativeFilePath);
            EnsureDirectoryExists(filePath);

            if (File.Exists(filePath)) {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            return new List<T>();
        }

        public void SaveToFile(List<T> items, string filePath) {
            try {
                // Check if directory exists, if not, create it
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine($"Created directory: {directory}");
                }

                // Serialize and save
                string json = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Saved to file: {filePath}");
            }
            catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        private string AdjustFilePath(string relativeFilePath) {
            return Path.Combine(@"..\..\..\", relativeFilePath);
        }

        private void EnsureDirectoryExists(string filePath) {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
