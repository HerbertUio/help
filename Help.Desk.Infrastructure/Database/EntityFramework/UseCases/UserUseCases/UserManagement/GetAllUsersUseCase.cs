using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Extensions;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

public class GetAllUsersUseCase: GenericRepository<UserEntity>
{
    private readonly HelpDeskDbContext _context;
    public GetAllUsersUseCase(HelpDeskDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<UserModel>> ExecuteGetAllAsync()
    {
       var items = await _context.Users.ToListAsync();
       return items.Select(userEntities => userEntities.EntityToModel()).ToList();
    }
}