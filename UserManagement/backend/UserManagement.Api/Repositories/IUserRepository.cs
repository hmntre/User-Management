using UserManagement.Api.Models;

namespace UserManagement.Api.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null);
}