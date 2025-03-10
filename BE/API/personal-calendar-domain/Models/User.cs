namespace personal_calendar_domain.Models;

public class User
{
    public Guid UserId { get; private set; }
    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }
    public string? Role { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public List<Event>? Events { get; private set; }


    private User() { }

    public User(
        string name,
        string surname,
        string email,
        string password,
        string role
        )
    {
        UserId = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Role = role;
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
    }


    // public void UpdatePassword(string password)
    // {
    //     if (!string.IsNullOrEmpty(password))
    //     {
    //         Password = password;
    //         Updated = DateTime.UtcNow;
    //     }
    // }

    public void UpdateUser(string name, string surname, string? password = null)
    {
        Name = name;
        Surname = surname;
        Updated = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(password))
        {
            Password = password;
        }
    }

    // public void UpdateSurname(string? surname)
    // {
    //     if (!string.IsNullOrEmpty(surname))
    //     {
    //         Surname = surname;
    //         Updated = DateTime.UtcNow;
    //     }
    // }

    // public void UpdateEmail(string? email)
    // {
    //     if (!string.IsNullOrEmpty(email))
    //     {
    //         Email = email;
    //         Updated = DateTime.UtcNow;
    //     }
    // }



    // public static User CreateUser(
    //     string name,
    //     string surname,
    //     string email,
    //     string password)
    // {
    //     return new User(
    //         name,
    //         surname,
    //         email,
    //         password);
    // }
}