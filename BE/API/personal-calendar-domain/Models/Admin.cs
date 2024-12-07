namespace personal_calendar_domain.Models;



public class Admin
{
    public Guid AdminId { get; private set; }
    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }


    private Admin() { }
}