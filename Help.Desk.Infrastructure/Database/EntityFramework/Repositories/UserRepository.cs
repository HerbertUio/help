using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class UserRepository: GenericRepository<UserEntity>, IUserRepository
{
    private readonly HelpDeskDbContext _context;
    private readonly DbSet<UserEntity> _users;
    public UserRepository(HelpDeskDbContext context) : base(context)
    {
        _context = context;
        _users = context.Set<UserEntity>();
    }
    public async Task<UserDto> CreateAsync(UserDto entity)
    {
        var userEntity = new UserEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Password = entity.Password,
            Role = entity.Role
        };
        await _users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Password = userEntity.Password,
            Role = userEntity.Role
        };
        
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _users.ToListAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        }).ToList();
        
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _users.FindAsync(id);
        if (user == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };
    }

    public async Task<UserDto> UpdateAsync(UserDto entity)
    {
        var userEntity = await _users.FindAsync(entity.Id);
        if (userEntity == null)
        {
            return null;
        }
        userEntity.Name = entity.Name;
        userEntity.Email = entity.Email;
        userEntity.Password = entity.Password;
        userEntity.Role = entity.Role;

        _context.Entry(userEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return new UserDto
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Password = userEntity.Password,
            Role = userEntity.Role
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userEntity = await _users.FindAsync(id);
        if (userEntity == null)
        {
            return false;
        }
        _users.Remove(userEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role
        };
    }
}