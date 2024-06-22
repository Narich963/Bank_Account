using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlWork9.Controllers;

public class UserController : Controller
{
    private readonly Context _context;
    private readonly UserManager<User> _userManager;
    public UserController(Context context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index() => View(await _context.Users.ToListAsync());
}
