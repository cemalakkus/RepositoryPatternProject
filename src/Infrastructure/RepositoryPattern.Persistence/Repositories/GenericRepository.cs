using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Application.Interfaces.Repositories;
using RepositoryPattern.Domain.Common;
using RepositoryPattern.Persistence.Context;

namespace RepositoryPattern.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking()
                                        .ToListAsync();
    }

    public async Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Set<T>().Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .AsNoTracking()
                                        .ToListAsync();
    }    

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    
}
