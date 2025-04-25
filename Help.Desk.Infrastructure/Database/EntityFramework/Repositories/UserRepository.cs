using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class UserRepository
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly UpdateUseUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;
    
    public UserRepository(
        CreateUserUseCase createUserUseCase,
        GetUserByIdUseCase getUserByIdUseCase,
        GetAllUsersUseCase getAllUsersUseCase,
        DeleteUserUseCase deleteUserUseCase,
        UpdateUseUseCase updateUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }
    
    public async Task<UserModel> CreateUser(UserModel userModel)
    {
        var result = await _createUserUseCase.ExecuteCreateAsync(userModel);
        return result;
    }

    public async Task<UserModel> UpdateUser(UserModel userModel)
    {
        var result = await _updateUserUseCase.ExecuteUpdateAsync(userModel);
        return result;
    }

    public async Task<bool> DeleteUser(int userId)
    {
        var result = await _deleteUserUseCase.ExecuteDeleteAsync(userId);
        return result;
    }
    public async Task<UserModel> GetUserById(int userId)
    {
        var result = await _getUserByIdUseCase.ExecuteGetByIdAsync(userId);
        return result;
    }
    public async Task<List<UserModel>> GetAllUsers()
    {
        var result = await _getAllUsersUseCase.ExecuteGetAllAsync();
        return result;
    }
    
}