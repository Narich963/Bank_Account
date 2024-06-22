using System.ComponentModel.DataAnnotations.Schema;

namespace ControlWork9.Models;

public class Transaction
{
    public int Id { get; set; }

    public int? FromId { get; set; }
    [NotMapped]
    public User? FromUser { get; set; }

    public int ToId { get; set; }
    [NotMapped]
    public User? ToUser { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int Sum { get; set; }
}
