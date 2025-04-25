using Help.Desk.Domain.Models;

namespace Help.Desk.Domain.Factories;

public class UserFactory
{
    public UserModel CreateUserModel (
        int id,
        string name,
        string lastName,
        string phoneNumber,
        string email,
        string password,
        int departmentId,
        string role,
        bool active)
    {
        return new UserModel(
            id,
            name,
            lastName,
            phoneNumber,
            email,
            password,
            departmentId,
            role,
            active);
    }
}