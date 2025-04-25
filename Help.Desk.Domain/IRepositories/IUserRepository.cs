using Help.Desk.Domain.IRepositories.Common;
using Help.Desk.Domain.Models;

namespace Help.Desk.Domain.IRepositories;

public interface IUserRepository: IGenericRepository<UserModel>
{
    Task<UserModel?> GetUserByEmailAsync(string email);
}