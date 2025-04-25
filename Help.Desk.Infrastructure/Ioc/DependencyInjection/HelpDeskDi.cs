using Help.Desk.Application.Services;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Help.Desk.Infrastructure.Ioc.DependencyInjection;

public static class HelpDeskDi
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString)
                , "La cadena de conexión no puede ser nula ni estar vacía.");
        }
        serviceCollection.AddDbContext<HelpDeskDbContext>(options =>
            options.UseNpgsql(connectionString));
        return serviceCollection;
    }
    
    public static IServiceCollection RegisterServices (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<UserService>();
        return serviceCollection;
    }
    
}