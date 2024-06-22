using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Models;

public class User : IdentityUser<int>
{
    public int? UniqueNumber { get; set; }
    public int Balance { get; set; } = 10000;
}
