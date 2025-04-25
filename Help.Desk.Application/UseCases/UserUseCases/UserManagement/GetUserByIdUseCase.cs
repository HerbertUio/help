using FluentValidation;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases.UserManagement;

public class GetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserModel>> ExcecuteGetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<UserModel>.Failure(new List<string> { "Usuario no encontrado" }, "Usuario no encontrado");
        }
        return Result<UserModel>.Success(user, "Usuario encontrado");
    }
}