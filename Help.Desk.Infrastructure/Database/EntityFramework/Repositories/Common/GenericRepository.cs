using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

public class GenericRepository<TEntity> where TEntity : class
{
    protected readonly HelpDeskDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    
    public GenericRepository(HelpDeskDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }
    
    public virtual async Task<TEntity> CreateAsyncBase(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> UpdateAsyncBase(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> DeleteAsyncBase(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<TEntity> GetByIdAsyncBase(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public virtual async Task<List<TEntity>> GetAllAsyncBase()
    {
        return await _dbSet.ToListAsync();
    }
}