using ControlWork9.Models;
using ControlWork9.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ControlWork9.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly Context _context;
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, Context context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _userManager.FindByEmailAsync(model.Login) ?? await _userManager.FindByNameAsync(model.Login)
                ?? await _context.Users.FirstOrDefaultAsync(u => u.UniqueNumber.ToString() == model.Login);
            if (user != null)
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            ModelState.AddModelError("", "Неверный логин или пароль");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (_userManager.Users.Any(u => u.Email == model.Email || u.UserName == model.Username))
            {
                ModelState.AddModelError("", "Пользователь с тамим именем или эл. почтой уже существует");
                return View(model);
            }
            User? user = new()
            {
                Email = model.Email,
                UserName = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                user.UniqueNumber = await GenerateUniqueNumber();
                await _userManager.UpdateAsync(user);
                Console.WriteLine(user.UniqueNumber);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
    private async Task<int> GenerateUniqueNumber()
    {
        int number;
        do
        {
            number = new Random().Next(100000, 999999);
        }
        while (await _userManager.Users.AnyAsync(u => u.UniqueNumber == number));
        return number;
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
