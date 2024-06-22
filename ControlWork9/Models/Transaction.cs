using System.ComponentModel.DataAnnotations.Schema;

namespace ControlWork9.Models;

public class Transaction
{
    public int Id { get; set; }
    public int Sum { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public int? UserFromId { get; set; }
    public User? UserFrom { get; set; }

    public int? UserToId { get; set; }
    public User? UserTo { get; set; }

    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
}
