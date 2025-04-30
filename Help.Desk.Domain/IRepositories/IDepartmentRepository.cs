using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories.Common;

namespace Help.Desk.Domain.IRepositories;

public interface IDepartmentRepository: IGenericRepository<DepartmentDto>
{
    Task<DepartmentDto> GetByNameAsync(string name);
    Task<bool> IsDepartmentExist(string name);
}   