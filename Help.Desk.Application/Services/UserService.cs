using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Application.UseCases.UserUseCases.UserManagement;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.Services;

public class UserService
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;

    public UserService(
        CreateUserUseCase createUserUseCase,
        GetUserByIdUseCase getUserByIdUseCase,
        GetAllUsersUseCase getAllUsersUseCase,
        DeleteUserUseCase deleteUserUseCase,
        UpdateUserUseCase updateUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    public async Task<Result<UserModel>> CreateUser(CreateUserDto userDto)
    {
        var result = await _createUserUseCase.ExecuteCreateAsync(userDto);
        return result;
    }

    public async Task<Result<UserModel>> GetUserById(int id)
    {
        var result = await _getUserByIdUseCase.ExcecuteGetByIdAsync(id);
        return result;
    }

    public async Task<Result<List<UserModel>>> GetAllUsers()
    {
        var result = await _getAllUsersUseCase.ExecuteGetAllAsync();
        return result;
    }
    
    public async Task<Result<bool>> DeleteUser(int id)
    {
        var result = await _deleteUserUseCase.ExecuteDeleteAsync(id);
        return result;
    }
    
    public async Task<Result<UserModel>> UpdateUser(int id, CreateUserDto userDto)
    {
        var result = await _updateUserUseCase.ExecuteUpdateAsync(id, userDto);
        return result;
    }
}