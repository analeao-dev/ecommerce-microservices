using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public async Task<User> AddUser(User user)
    {
        user.Id = Guid.NewGuid();
        return user;
    }

    public async Task<User?> GetUserByEmailAndPassword(string? email, string? password)
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = password,
            Name = "Ana",
            Gender = nameof(GenderOptions.Female)
        };
    }
}