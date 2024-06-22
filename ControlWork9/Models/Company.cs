namespace ControlWork9.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<CompanyUser> CompanyUser { get; set; }
    public Company()
    {
        CompanyUser = new();
    }
}
