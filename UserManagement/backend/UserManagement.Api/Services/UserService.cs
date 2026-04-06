using UserManagement.Api.Models;
using UserManagement.Api.DTOs;
using UserManagement.Api.Repositories;

namespace UserManagement.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.ToList();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        if (!await _userRepository.IsEmailUniqueAsync(dto.Email))
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.SetPassword(dto.Password);

        return await _userRepository.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        if (!await _userRepository.IsEmailUniqueAsync(dto.Email, id))
        {
            throw new InvalidOperationException("Email already exists");
        }

        user.Name = dto.Name;
        user.Email = dto.Email;
        user.Role = dto.Role;
        user.IsActive = dto.IsActive;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.IsActive = !user.IsActive;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public List<User> GetAll() => GetAllAsync().GetAwaiter().GetResult();
    public User? GetById(Guid id) => GetByIdAsync(id).GetAwaiter().GetResult();
    public User Create(CreateUserDto dto) => CreateAsync(dto).GetAwaiter().GetResult();
    public bool Update(Guid id, UpdateUserDto dto) => UpdateAsync(id, dto).GetAwaiter().GetResult();
    public bool Delete(Guid id) => DeleteAsync(id).GetAwaiter().GetResult();
    public bool ToggleActive(Guid id) => ToggleActiveAsync(id).GetAwaiter().GetResult();
}
