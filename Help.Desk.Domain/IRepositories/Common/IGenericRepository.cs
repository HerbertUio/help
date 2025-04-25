namespace Help.Desk.Domain.IRepositories.Common;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(int id);
    Task<List<TEntity?>> GetAllAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
}