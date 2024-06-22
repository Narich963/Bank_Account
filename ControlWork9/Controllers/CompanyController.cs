using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlWork9.Controllers;

public class CompanyController : Controller
{
    private readonly Context _context;
    private readonly UserManager<User> _userManager;

    public CompanyController(Context context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Companн
    public async Task<IActionResult> Index()
    {
        return View(await _context.Companies.ToListAsync());
    }

    // GET: Companн/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.Companies
            .Include(c => c.CompanyUser)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        User user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var companyUser = company.CompanyUser.FirstOrDefault(c => c.UserId == id);
            if (companyUser != null)
            {
                ViewBag.Balance = companyUser.Balance;
            }
        }

        return View(company);
    }
}
