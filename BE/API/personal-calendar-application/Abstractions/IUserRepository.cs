using personal_calendar_domain.Models;

namespace personal_calendar_application.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User> UpdateUserAsync(User user);
    Task<User?> DeleteUserByIdAsync(Guid id);
    Task<User> CreateUserAsync(User user);
    Task<IEnumerable<Event>> GetUserEventsByIdAsync(Guid id);
    Task<bool> IsEmailPresentInDb(string email);
    Task<User?> GetUserByEmail(string email);
}