using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;
using Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class UserRepository: GenericRepository<UserEntity>, IUserRepository
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly UpdateUseUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;


    public UserRepository(HelpDeskDbContext context, CreateUserUseCase createUserUseCase, GetUserByIdUseCase getUserByIdUseCase, GetAllUsersUseCase getAllUsersUseCase, UpdateUseUseCase updateUserUseCase, DeleteUserUseCase deleteUserUseCase) : base(context)
    {
        _createUserUseCase = createUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    public async Task<UserModel> CreateAsync(UserModel userModel)
    {
        var result = await _createUserUseCase.ExecuteCreateAsync(userModel);
        return result;
    }
    
    public async Task<UserModel> UpdateAsync(UserModel userModel)
    {
        var result = await _updateUserUseCase.ExecuteUpdateAsync(userModel);
        return result;
    }
    public async Task<bool> DeleteAsync(int userId)
    {
        var result = await _deleteUserUseCase.ExecuteDeleteAsync(userId);
        return result;
    }
    
    public async Task<UserModel?> GetByIdAsync(int userId)
    {
        var result = await _getUserByIdUseCase.ExecuteGetByIdAsync(userId);
        return result;
    }
    public async Task<List<UserModel>> GetAllAsync()
    {
        var result = await _getAllUsersUseCase.ExecuteGetAllAsync();
        return result;
    }
    
    public Task<UserModel?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}