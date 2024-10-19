namespace AppColegioPG.Services.IServices
{
    public interface IMetodos<T>
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>>GetAll();

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete(int id);
    }
}
