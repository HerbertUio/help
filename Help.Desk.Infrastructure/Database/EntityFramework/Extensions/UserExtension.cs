using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Extensions;

public static class UserExtension
{
    public static UserEntity ModelToEntity(this UserModel model)
    {
        return new UserEntity()
        {
            Id = model.Id,
            Name = model.Name,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            Password = model.Password,
            DepartmentId = model.DepartmentId,
            Role = model.Role,
            Active = model.Active
        };
    }

    public static UserModel EntityToModel(this UserEntity entity)
    {
        return new UserModel
        (
            entity.Id,
            entity.Name,
            entity.LastName,
            entity.PhoneNumber,
            entity.Email,
            entity.Password,
            entity.DepartmentId,
            entity.Role,
            entity.Active
        );
    }
}