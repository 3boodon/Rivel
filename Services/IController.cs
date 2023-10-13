using System.Collections.Generic;


namespace Rivel.Services
{

    public interface IController<T>
    {
        /// <summary>
        /// Retrieves all records from the database.
        /// </summary>
        /// <returns>A list of all records.</returns>
        List<T> GetAll();

        /// <summary>
        /// Retrieves a single record by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to retrieve.</param>
        /// <returns>The record with the specified ID.</returns>
        T GetById(int id);


        /// <summary>
        /// Updates a record by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if the update is successful, otherwise false.</returns>
        bool Update(int id, T entity);

        /// <summary>
        /// Deletes a record by its ID.
        /// </summary>
        /// <param name="id">The ID of the record to delete.</param>
        /// <returns>True if the deletion is successful, otherwise false.</returns>
        bool Delete(int id);

        /// <summary>
        /// Creates a new record.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The ID of the newly created record.</returns>
        bool Create(T entity);
    }

}
