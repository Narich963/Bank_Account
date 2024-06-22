using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Models;

public class User : IdentityUser<int>
{
    public int? UniqueNumber { get; set; }
    public int Balance { get; set; } = 10000;
    public List<Transaction> SendTransactions { get; set; }
    public List<Transaction> ReceivedTransactions { get; set; }
    public User()
    {
        SendTransactions = new List<Transaction>();
        ReceivedTransactions = new List<Transaction>();
    }
}
