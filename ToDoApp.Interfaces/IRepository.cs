namespace ToDoApp.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<ICollection<T>> GetAll();
        Task<T> AddItem(T entity);
        Task<T> UpdateItem(T entity);
        Task<bool> DeleteItem(int id);
        Task<T> GetItemById(int id);
    }
}