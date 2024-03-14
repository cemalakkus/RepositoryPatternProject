using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Application.Interfaces.Repositories;
using RepositoryPattern.Persistence.Context;
using RepositoryPattern.Persistence.Repositories;

namespace RepositoryPattern.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration.GetSection("UsePostgreSqlDatabase").Value))
                service.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql"), 
                                                         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            else
                service.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MsSql"), 
                                                         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
