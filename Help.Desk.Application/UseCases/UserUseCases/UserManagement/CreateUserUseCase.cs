using FluentValidation;
using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Domain.Factories;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases.UserManagement;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateUserDto> _validator;
    private readonly UserFactory _userFactory;
    
    public CreateUserUseCase(IUserRepository userRepository, IValidator<CreateUserDto> validator, UserFactory userFactory)
    {
        _userFactory = userFactory;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<Result<UserModel>> ExecuteCreateAsync(CreateUserDto user)
    {
        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return Result<UserModel>
                .Failure(validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList(), "Datos de usuario invalidos");
        }
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            return Result<UserModel>
                .Failure(new List<string> { "El email ya está en uso" }, "Email ya en uso");
        }
        var newUser = _userFactory.CreateUserModel(0,
            user.Name,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Password,
            user.DepartmentId,
            user.Role,
            true);
        var createdUser = await _userRepository.CreateAsync(newUser);
        if (createdUser == null)
        {
            return Result<UserModel>
                .Failure(new List<string> { "Error al crear el usuario" }, "Error al crear el usuario");
        }
        return Result<UserModel>
            .Success(createdUser, "Usuario creado con éxito");
    }
}