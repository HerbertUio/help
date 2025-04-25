using FluentValidation;
using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Application.Services;
using Help.Desk.Application.UseCases.UserUseCases.UserManagement;
using Help.Desk.Application.Validators.UserValidators;
using Help.Desk.Domain.Factories;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
// Asegúrate que el namespace del repositorio concreto sea correcto
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories; // ASUMIENDO que aquí está UserRepository CONCRETO
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// Importa los namespaces de los UseCases de Infrastructure si son diferentes
using InfraUserCreate = Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement.CreateUserUseCase;
using InfraUserDelete = Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement.DeleteUserUseCase;
using InfraUserGetAll = Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement.GetAllUsersUseCase;
using InfraUserGetById = Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement.GetUserByIdUseCase;
using InfraUserUpdate = Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement.UpdateUseUseCase;


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
        
        // Registrar la factoría para las herramientas EF Core
        serviceCollection.AddDbContextFactory<HelpDeskDbContext>();
        serviceCollection.AddScoped<IDesignTimeDbContextFactory<HelpDeskDbContext>, HelpDeskDbContextFactory>();


        return serviceCollection;
    }
    
    // Método para registrar todas las dependencias de la aplicación e infraestructura
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        // Domain
        services.AddScoped<UserFactory>();

        // Application - Use Cases (los que usan los servicios)
        services.AddScoped<Help.Desk.Application.UseCases.UserUseCases.UserManagement.CreateUserUseCase>();
        services.AddScoped<Help.Desk.Application.UseCases.UserUseCases.UserManagement.DeleteUserUseCase>();
        services.AddScoped<Help.Desk.Application.UseCases.UserUseCases.UserManagement.GetAllUsersUseCase>();
        services.AddScoped<Help.Desk.Application.UseCases.UserUseCases.UserManagement.GetUserByIdUseCase>();
        services.AddScoped<Help.Desk.Application.UseCases.UserUseCases.UserManagement.UpdateUserUseCase>();

        // Application - Services
        services.AddScoped<UserService>();

        // Application - Validators (Registrados en Program.cs ya, pero se puede mover aquí)
        // services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
        // services.AddScoped<IValidator<UserLoginDto>, LoginUserValidator>();
        // FluentValidation los registra automáticamente con AddValidatorsFromAssemblyContaining

        // Infrastructure - Repositories (Interfaz -> Implementación)
        // NECESITAS UNA IMPLEMENTACIÓN CONCRETA QUE IMPLEMENTE IUserRepository
        // ASUMIENDO que tienes una clase UserRepository que implementa IUserRepository y usa los UseCases de Infraestructura
         
        
        services.AddScoped<IUserRepository, UserRepository>(); // REEMPLAZA con tu implementación real si es diferente

        // Infrastructure - Use Cases (los que interactúan directamente con EF)
        services.AddScoped<InfraUserCreate>();
        services.AddScoped<InfraUserDelete>();
        services.AddScoped<InfraUserGetAll>();
        services.AddScoped<InfraUserGetById>();
        services.AddScoped<InfraUserUpdate>();
        
        // TODO: Registrar otros repositorios y servicios (Tickets, KB, etc.) aquí cuando los crees

        return services;
    }

    // NOTA: El antiguo RegisterServices solo registraba UserService, ahora RegisterDependencies es más completo.
    // Puedes eliminar el antiguo si lo prefieres.
    // public static IServiceCollection RegisterServices (this IServiceCollection serviceCollection)
    // {
    //     serviceCollection.AddTransient<UserService>();
    //     return serviceCollection;
    // }
    
}