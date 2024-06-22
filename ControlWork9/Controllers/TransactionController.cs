using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Controllers;

public class TransactionController : Controller
{
    private readonly Context _context;
    private readonly UserManager<User> _userManager;

    public TransactionController(Context context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Transaction
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        User user = await _userManager.GetUserAsync(User);
        var transactions = await _context.Transactions
            .Include(u => u.UserTo)
            .Include(u => u.UserFrom)
            .Include(u => u.Company)
            .Where(u => u.UserToId == user.Id || u.UserFromId == user.Id)
            .ToListAsync();

        if (from.HasValue)
        {
            transactions = transactions.Where(t => t.Created >= from).ToList();
        }
        if (to.HasValue)
        {
            transactions = transactions.Where(t => t.Created <= to).ToList();
        }
        transactions = transactions.OrderByDescending(t => t.Created).ToList();
        return View(transactions);
    }
}
