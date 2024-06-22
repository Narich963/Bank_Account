namespace ControlWork9.Models;

public class CompanyUser
{
    public int Id { get; set; }
    public int Balance { get; set; } = 0;

    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
