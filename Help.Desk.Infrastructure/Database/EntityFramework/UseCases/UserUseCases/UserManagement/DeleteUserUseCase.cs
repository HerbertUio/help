using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

public class DeleteUserUseCase: GenericRepository<UserEntity>
{
    private readonly HelpDeskDbContext _context;
    public DeleteUserUseCase(HelpDeskDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<bool> ExecuteDeleteAsync(int id)
    {
        var userEntity = await base.GetByIdAsyncBase(id);
        if (userEntity == null)
        {
            return false;
        }
        
        await base.DeleteAsyncBase(userEntity);
        await _context.SaveChangesAsync();
        return true;
    }
}