using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases.UserManagement;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Result<bool>> ExecuteDeleteAsync(int id)
    {
        var deleted = await _userRepository.DeleteAsync(id);
        if (deleted)
        {
            return Result<bool>.Success(true, "Usuario eliminado");
        }
        return Result<bool>.Failure(new List<string> { "No se pudo eliminar el usuario" }, "No se pudo eliminar el usuario");
    }
}