using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Extensions;
using OnlineStore.Models.Account;
using OnlineStore.Repositories;
using reCAPTCHA.AspNetCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IRecaptchaService recaptchaService;
        private readonly IOrderRepository orderRepository;

        public AccountController(IAccountRepository accountRepository, 
                                 IRecaptchaService recaptchaService, 
                                 IOrderRepository orderRepository)
        {
            this.accountRepository = accountRepository;
            this.recaptchaService = recaptchaService;
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var userAddresses = await accountRepository.FindUserAddressesAsync(userId);
            var latestOrder = await orderRepository.GetOrderListWithProductsAsync(userId, 1, 1);

            var viewModel = new AccountViewModel
            {
                ShippingDetails = userAddresses?
                    .FirstOrDefault(ua => ua.AddressType == AddressType.DeliveryAddress),
                UserDetails = userAddresses?
                    .FirstOrDefault(ua => ua.AddressType == AddressType.Address),
                OrderDetails = latestOrder?.FirstOrDefault()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Orders(int page = 1, int pageSize = 5)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var ordersList = await orderRepository.GetOrderListWithProductsAsync(userId, page, pageSize);
            var ordersCount = await orderRepository.CountOrdersAsync(userId);

            return View(new StaticPagedList<Order>(ordersList, page, pageSize, ordersCount));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Settings(AddressType addressType = AddressType.Address)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            var userAddresses = await accountRepository.FindUserAddressesAsync(userId);


            ViewData["AddressType"] = addressType;
            var viewModel = new AddressViewModel
            {
                ShippingDetails = userAddresses?
                    .FirstOrDefault(ua => ua.AddressType == AddressType.DeliveryAddress),
                UserDetails = userAddresses?
                    .FirstOrDefault(ua => ua.AddressType == AddressType.Address)
            };
            return View(viewModel);
        }

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
                var logged = await accountRepository
                    .SignInAsync(loginDetails.Email, loginDetails.Password);

                if (logged)
                {
                    return RedirectToAction(nameof(HomeController.Index),
                        nameof(HomeController).RemoveController());
                }
            }
            ModelState.AddModelError("", "Nieprawidłowy email lub hasło.");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings([FromForm] SettingsModel details,
                                                  [FromQuery] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                await accountRepository.AddOrUpdateAddressAsync(userId, 
                    new Address(details), addressType);
            }
            return RedirectToAction(nameof(Settings), new { addressType });
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await accountRepository.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
