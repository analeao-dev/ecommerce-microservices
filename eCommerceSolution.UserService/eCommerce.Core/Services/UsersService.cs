using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using FluentValidation;

namespace eCommerce.Core.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<LoginRequest> _loginRequestValidator;
    private readonly IValidator<RegisterRequest> _registerRequestValidator;

    public UsersService(
        IUsersRepository repository,
        IMapper mapper,
        IValidator<LoginRequest> loginRequestValidator,
        IValidator<RegisterRequest> registerRequestValidator)
    {
        _usersRepository = repository;
        _mapper = mapper;
        _loginRequestValidator = loginRequestValidator;
        _registerRequestValidator = registerRequestValidator;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        var validationResult = await _loginRequestValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (user == null) return null;

        return _mapper.Map<AuthenticationResponse>(user) with { Success = true , Token = "token" };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        var validationResult = await _registerRequestValidator.ValidateAsync(registerRequest);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var user = _mapper.Map<User>(registerRequest);
        
        var registeredUser = await _usersRepository.AddUser(user);

        if (registeredUser == null) return null;
        
        return _mapper.Map<AuthenticationResponse>(registeredUser) with { Success = true, Token = "token" };
    }
}