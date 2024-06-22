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

    // GET: Companн/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Companн/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Company company)
    {
        if (ModelState.IsValid)
        {
            _context.Add(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }

    // GET: Companн/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }
        return View(company);
    }

    // POST: Companн/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Company company)
    {
        if (id != company.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(company);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(company.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(company);
    }

    // GET: Companн/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.Companies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // POST: Companн/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company != null)
        {
            _context.Companies.Remove(company);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CompanyExists(int id)
    {
        return _context.Companies.Any(e => e.Id == id);
    }
}
