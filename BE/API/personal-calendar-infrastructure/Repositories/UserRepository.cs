using Microsoft.EntityFrameworkCore;
using personal_calendar_application.Abstractions;
using personal_calendar_domain.Models;
using personal_calendar_infrastructure.Database;

namespace personal_calendar_infrastructure.Repositories;


public class UserRepository : IUserRepository
{
    private readonly CalendarDbContext _dbContext;

    public UserRepository(CalendarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUserByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null) return null; // Throw proper exception
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users.Where(u => u.Role == "User").ToListAsync();
        return users;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user == null) return null; // handle null or exception
        return user;
    }

    public async Task<IEnumerable<Event>?> GetUserEventsByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.UserId == id);
        if (user is null || user.Events is null) return null;
        return user.Events;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> IsEmailPresentInDb(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

}