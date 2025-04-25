using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Extensions;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

public class UpdateUseUseCase: GenericRepository<UserEntity>

{
    private readonly HelpDeskDbContext _context;
    public UpdateUseUseCase(HelpDeskDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<UserModel> ExecuteUpdateAsync(UserModel userModel)
    {
        var userEntity = userModel.ModelToEntity();
        var updatedEntity = await base.UpdateAsyncBase(userEntity);
        await _context.SaveChangesAsync();
        return updatedEntity.EntityToModel();
    }
    
}