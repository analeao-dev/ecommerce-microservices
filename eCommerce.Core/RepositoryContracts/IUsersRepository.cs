using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.RepositoryContracts;

public interface IUsersRepository
{
    Task<User> AddUser(User user);
    Task<User?> GetUserByEmailAndPassword(string? email, string? password);
}