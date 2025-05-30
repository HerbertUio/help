using FluentValidation;
using Help.Desk.Application.Services;
using Help.Desk.Application.UseCases.UserUseCases;
using Help.Desk.Application.Validators.UserValidators;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.IRepositories.Common;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Help.Desk.Infrastructure.Ioc;

public static class HelpDeskDependencyInjection
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection collection, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];
        collection.AddDbContext<HelpDeskDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return collection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        collection.AddTransient<UserService>();
        collection.AddTransient<CreateUserUseCase>();
        collection.AddTransient<UpdateUserUseCase>();
        collection.AddTransient<DeleteUserUseCase>();
        collection.AddTransient<GetUserByIdUseCase>();
        collection.AddTransient<GetAllUsersUseCase>();
        collection.AddTransient<GetByEmailUseCase>();
        
        return collection;
    }
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<IUserRepository, UserRepository>();
        return collection;
    }
    public static IServiceCollection RegisterValidators(this IServiceCollection collection)
    {
        collection.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

        return collection;
    }
}