using UserManagement.Api.Models;
using UserManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace UserManagement.Api.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IMemoryCache _cache;
    private const string USERS_CACHE_KEY = "all_users";
    private const string USER_CACHE_KEY = "user_{0}";
    private const int CACHE_DURATION_MINUTES = 30;

    public UserRepository(ApplicationDbContext context, IMemoryCache cache) : base(context)
    {
        _cache = cache;
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        if (_cache.TryGetValue(USERS_CACHE_KEY, out IEnumerable<User>? cachedUsers) && cachedUsers != null)
        {
            return cachedUsers;
        }

        var users = await base.GetAllAsync();
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));

        _cache.Set(USERS_CACHE_KEY, users, cacheOptions);
        return users;
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        var cacheKey = string.Format(USER_CACHE_KEY, id);
        if (_cache.TryGetValue(cacheKey, out User? cachedUser))
        {
            return cachedUser;
        }

        var user = await base.GetByIdAsync(id);
        if (user != null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));
            _cache.Set(cacheKey, user, cacheOptions);
        }

        return user;
    }

    public override async Task<User> AddAsync(User entity)
    {
        var result = await base.AddAsync(entity);
        InvalidateUsersCaches();
        return result;
    }

    public override async Task UpdateAsync(User entity)
    {
        await base.UpdateAsync(entity);
        InvalidateUsersCaches();
        InvalidateUserCache(entity.Id);
    }

    public override async Task DeleteAsync(User entity)
    {
        await base.DeleteAsync(entity);
        InvalidateUsersCaches();
        InvalidateUserCache(entity.Id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await FindAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await FindAllAsync(u => u.IsActive);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        return await FindAllAsync(u => u.Role == role);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeUserId = null)
    {
        var query = _dbSet.Where(u => u.Email.ToLower() == email.ToLower());

        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }

        return !await query.AnyAsync();
    }

    private void InvalidateUsersCaches()
    {
        _cache.Remove(USERS_CACHE_KEY);
    }

    private void InvalidateUserCache(Guid userId)
    {
        _cache.Remove(string.Format(USER_CACHE_KEY, userId));
    }
}