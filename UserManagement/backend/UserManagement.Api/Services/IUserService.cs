using UserManagement.Api.Models;
using UserManagement.Api.DTOs;

namespace UserManagement.Api.Services;

public interface IUserService
{
    // Async methods
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(CreateUserDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateUserDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ToggleActiveAsync(Guid id);

    // Sync methods (for backward compatibility)
    List<User> GetAll();
    User? GetById(Guid id);
    User Create(CreateUserDto dto);
    bool Update(Guid id, UpdateUserDto dto);
    bool Delete(Guid id);
    bool ToggleActive(Guid id);
}
