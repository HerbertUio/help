using FluentValidation;
using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Domain.Factories;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases.UserManagement;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserDto> _createUserValidator;
    private readonly UserFactory _userFactory;
    
    public UpdateUserUseCase(IUserRepository userRepository, IValidator<CreateUserDto> createUserValidator, UserFactory userFactory)
    {
        _userFactory = userFactory;
        _userRepository = userRepository;
        _createUserValidator = createUserValidator;
    }

    public async Task<Result<UserModel>> ExecuteUpdateAsync(int id, CreateUserDto createUserDto)
    {
        var validationResult = await _createUserValidator.ValidateAsync(createUserDto);
        if (!validationResult.IsValid)
        {
            return Result<UserModel>
                .Failure(validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList(), "Los datos proporcionados no son válidos para actualizar la persona.");
        }
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<UserModel>.Failure(new List<string> { "Usuario no encontrado" }, "Usuario no encontrado");
        }

        var updateUserDto = _userFactory.CreateUserModel(
            user.Id,
            createUserDto.Name,
            createUserDto.LastName,
            createUserDto.PhoneNumber,
            createUserDto.Email,
            createUserDto.Password,
            createUserDto.DepartmentId,
            createUserDto.Role,
            true);
        var updatedUser = await _userRepository.UpdateAsync(updateUserDto);
        if (updatedUser == null)
        {
            return Result<UserModel>.Failure(new List<string> { "Error al actualizar el usuario" }, "Error al actualizar el usuario");
        }
        return Result<UserModel>.Success(updatedUser, "Usuario actualizado con éxito");
    }
}