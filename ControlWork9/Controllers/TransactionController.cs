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

    // GET: Transaction/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transaction/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Transaction/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FromId,ToId,Created,Sum")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }

    // GET: Transaction/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return View(transaction);
    }

    // POST: Transaction/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FromId,ToId,Created,Sum")] Transaction transaction)
    {
        if (id != transaction.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
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
        return View(transaction);
    }

    // GET: Transaction/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.Id == id);
    }
}
