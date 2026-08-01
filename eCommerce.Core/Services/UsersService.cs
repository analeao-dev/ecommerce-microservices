using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository repository)
    {
        _usersRepository = repository;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        var user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (user == null) return null;

        return new AuthenticationResponse(user.Id, user.Email, user.Name, user.Gender, "token", Success: true);
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        var user = new User()
        {
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Gender = nameof(registerRequest.Gender)
        };
        var registeredUser = await _usersRepository.AddUser(user);

        if (registeredUser == null) return null;

        return new AuthenticationResponse(registeredUser.Id, registeredUser.Email, registeredUser.Name, "token",
            registeredUser.Gender, Success: true);
    }
}