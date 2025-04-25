using Help.Desk.Application.Dtos.UserDtos;
using Help.Desk.Application.Services;
using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Help.Desk.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;
    public UserController(UserService userService, ILogger<UserController> logger)
    {
        this._userService = userService;
        this._logger = logger;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<UserEntity>))]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "El error ocurrio al obtener todos los usuarios.");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(200, Type = typeof(UserEntity))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting user by id.");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(UserDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (createUserDto == null)
        {
            return BadRequest("El usuario no puede ser null");
        }
        try
        {
            var createdUser = await _userService.CreateUser(createUserDto);
            return CreatedAtRoute("GetUserById", new { id = createdUser.IsSuccess }, createdUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "El error ocurrio al crear el usuario.");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpPut("{id:int}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto createUserDto)
    {
        if (createUserDto == null)
        {
            return BadRequest("El usuario no puede ser null");
        }
        try
        {
            var updatedUser = await _userService.UpdateUser(id, createUserDto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "El error ocurrio al actualizar el usuario.");
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpDelete("{id:int}", Name = "DeleteUser")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _userService.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "El error ocurrio al eliminar el usuario.");
            return StatusCode(500, "Internal server error");
        }
    }
    
}