using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DapperDbContext _dbContext;
    
    public UsersRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> AddUser(User user)
    {
        user.UserId = Guid.NewGuid();
        
        const string query = @"INSERT INTO ""Users"" (""UserId"", ""Email"", ""Password"", ""Name"", ""Gender"") values (@UserId, @Email, @Password, @Name, @Gender)";
        
        var rowsAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);
        
        return rowsAffected == 0 ? null : user;
    }

    public async Task<User?> GetUserByEmailAndPassword(string? email, string? password)
    {
        var parameters = new { Email = email, Password = password };
        const string query = @"SELECT * FROM ""Users"" WHERE ""Email"" = @Email AND ""Password"" = @Password";

        var user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<User>(query, parameters);
        return user;
    }
}