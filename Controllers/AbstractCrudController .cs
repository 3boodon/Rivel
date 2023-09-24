using System;
using System.Collections.Generic;
using Rivel.Models;
using Rivel.Services;

namespace Rivel.Controllers {
    internal abstract class AbstractCrudController<T> : ICrudController<T> where T : BaseModel {
        protected readonly string FilePath;
        protected readonly FileService<T> FileService;

        protected AbstractCrudController(string filePath) {
            FilePath = filePath;
            FileService = new FileService<T>();
        }

        public void Create(T newItem) {
            newItem.DateCreated = DateTime.Now;
            newItem.LastUpdated = DateTime.Now;

            List<T> items = Read();
            items.Add(newItem);
            FileService.SaveToFile(items, FilePath);
        }

        public List<T> Read() {
            return FileService.ReadFromFile(FilePath);
        }

        public void Update(int id, T updatedItem) {
            List<T> items = Read();
            int index = items.FindIndex(i => i.Id == id);
            if (index != -1) {
                updatedItem.LastUpdated = DateTime.Now;
                items[index] = updatedItem;
                FileService.SaveToFile(items, FilePath);
            }
        }

        public void Delete(int id) {
            List<T> items = Read();
            items.RemoveAll(i => i.Id == id);
            FileService.SaveToFile(items, FilePath);
        }
    }
}
