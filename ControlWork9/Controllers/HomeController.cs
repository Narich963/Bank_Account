using ControlWork9.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ControlWork9.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly Context _context;
        public HomeController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToBalance(int sum, int uniqueNumber)
        {
            if (sum <= 0)
            {
                TempData["Error"] = "Недопустимое значение пополнения";
                return RedirectToAction("Index");
            }
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UniqueNumber ==  uniqueNumber);
            if (user != null)
            {
                user.Balance += sum;
                Transaction tr = new()
                {
                    UserToId = user.Id,
                    Sum = sum,
                };
                await _context.AddAsync(tr);
                _context.Update(user);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Баланс успешно пополнен";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Данный номер не был найден";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(int sum, int uniqueNumber)
        {
            if (sum <= 0)
            {
                TempData["Error"] = "Недопустимое значение пополнения";
                return RedirectToAction("Index");
            }
            User fromUser = await _userManager.GetUserAsync(User);
            User toUser = await _context.Users.FirstOrDefaultAsync(u => u.UniqueNumber == uniqueNumber);
            if (fromUser != null && toUser != null)
            {
                if (fromUser.Balance < sum)
                {
                    TempData["Error"] = "На балансе не достаточно средств";
                    return RedirectToAction("Index");
                }
                fromUser.Balance -= sum;
                toUser.Balance += sum;

                Transaction tr = new()
                {
                    UserFromId = fromUser.Id,
                    UserToId = toUser.Id,
                    Sum = sum
                };

                await _context.AddAsync(tr);
                _context.UpdateRange(fromUser, toUser);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Деньги успешно переведены";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Данный номер не был найден";
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
