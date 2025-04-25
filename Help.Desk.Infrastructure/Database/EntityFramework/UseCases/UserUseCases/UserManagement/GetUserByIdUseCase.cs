using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Extensions;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

public class GetUserByIdUseCase: GenericRepository<UserEntity>
{
    private readonly HelpDeskDbContext _context;
    public GetUserByIdUseCase(HelpDeskDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<UserModel> ExecuteGetByIdAsync(int id)
    {
        var userEntity = await base.GetByIdAsyncBase(id);
        return userEntity.EntityToModel();
    }
}