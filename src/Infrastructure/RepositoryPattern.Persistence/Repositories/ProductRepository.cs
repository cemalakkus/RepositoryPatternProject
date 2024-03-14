using RepositoryPattern.Application.Interfaces.Repositories;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Persistence.Context;

namespace RepositoryPattern.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
