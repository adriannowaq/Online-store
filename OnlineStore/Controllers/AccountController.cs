using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.Account;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [HttpGet]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            return Json(!await accountRepository.CheckEmailExistsAsync(email));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel loginDetails)
        {
            if (ModelState.IsValid)
            {
                var logged = await accountRepository.SignInAsync(loginDetails.Email, loginDetails.Password);
                if (logged)
                    return RedirectToAction("Home", nameof(HomeController.Index));
            }
            ModelState.AddModelError("", "Nieprawidłowy email lub hasło.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel registerDetails)
        {
            if (ModelState.IsValid)
            {
                var accountCreated = await accountRepository
                    .CreateAccountAsync(registerDetails.Email, registerDetails.Password);
                if (accountCreated)
                    return RedirectToAction(nameof(Login));
                else
                    ModelState.AddModelError("", "Adres email jest już zajęty.");
            }
            return View();
        }
    }
}
