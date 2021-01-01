using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Models.Account;
using OnlineStore.Repositories;
using reCAPTCHA.AspNetCore;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IRecaptchaService recaptchaService;

        public AccountController(IAccountRepository accountRepository, IRecaptchaService recaptchaService)
        {
            this.accountRepository = accountRepository;
            this.recaptchaService = recaptchaService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterModel registerDetails)
        {
            var recaptcha = await recaptchaService.Validate(Request);
            if (!recaptcha.success || recaptcha.score < 0.7)
            {
                ModelState.AddModelError("",
                    "Nie przeszedłeś poprawnie weryfikacji reCAPTCHA, spróbuj odświeżyć stronę");
                return View();
            }

            if (ModelState.IsValid)
            {
                var accountCreated = await accountRepository
                    .CreateAccountAsync(registerDetails.Email, registerDetails.Password, UserRole.User);
                if (accountCreated)
                {
                    TempData["RegisterCompleted"] = "Poprawnie utworzono konto!";
                    return RedirectToAction(nameof(Login));
                }
                else
                    ModelState.AddModelError("", "Adres email jest już zajęty.");
            }
            return View();
        }
    }
}
