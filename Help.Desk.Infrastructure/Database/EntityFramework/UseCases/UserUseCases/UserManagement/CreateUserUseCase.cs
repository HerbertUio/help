using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Extensions;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Help.Desk.Infrastructure.Database.EntityFramework.UseCases.UserUseCases.UserManagement;

public class CreateUserUseCase: GenericRepository<UserEntity>
{
    private readonly HelpDeskDbContext _context;

    public CreateUserUseCase(HelpDeskDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<UserModel> ExecuteCreateAsync(UserModel userModel)
    {
        var userEntity = userModel.ModelToEntity();
        if (userEntity.Id == 0)
        {
            var createdEntity = await base.CreateAsyncBase(userEntity);
            await _context.SaveChangesAsync();
            return createdEntity.EntityToModel();
        }

        var updatedEntity = await base.UpdateAsyncBase(userEntity);
        await _context.SaveChangesAsync();
        return updatedEntity.EntityToModel();
    }
}