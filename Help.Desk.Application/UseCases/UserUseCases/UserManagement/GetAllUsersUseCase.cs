using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases.UserManagement;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    
    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<List<UserModel>>> ExecuteGetAllAsync()
    {
        var result = await _userRepository.GetAllAsync();
        if (result == null || result.Count == 0)
        {
            return Result<List<UserModel>>
                .Failure(new List<string> { "No se encontraron usuarios" }
                    , "No se encontraron usuarios");
        }
        return Result<List<UserModel>>.Success(result, "Usuarios encontrados");
    }
}