using System.Collections.Generic;

namespace Rivel.Services {
    internal interface ICrudController<T> {
        void Create(T newItem);
        List<T> Read();
        void Update(int id, T updatedItem);
        void Delete(int id);
    }
}
