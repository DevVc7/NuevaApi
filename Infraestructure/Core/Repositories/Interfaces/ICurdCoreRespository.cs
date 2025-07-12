namespace Infraestructure.Core.Repositories.Interfaces
{
    public interface ICurdCoreRespository<T, ID>
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<T?> FindByIdAsync(ID id);
        Task<T> SaveAsync(T entity);

    }
}
