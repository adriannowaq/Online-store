using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Extensions;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Models.Cart;
using OnlineStore.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;
        private readonly IAccountRepository accountRepository;
        private readonly IOrderRepository orderRepository;

        public CartController(ICartService cartService, 
                              IAccountRepository accountRepository,
                              IOrderRepository orderRepository)
        {
            this.cartService = cartService;
            this.accountRepository = accountRepository;
            this.orderRepository = orderRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Completed(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await cartService.GetCartAsync();
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Finalization()
        {
            var cart = await cartService.GetCartAsync();
            if (cart?.CartItems.Count > 0)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                var order = await orderRepository.GetUncompletedOrderAsync(userId, cart.Id);
                if (order?.AddressesCompleted == false)
                    return RedirectToAction(nameof(Payment), new { order.Id });

                if (order != null)
                {
                    var orderProducts = new List<OrderProduct>();
                    foreach (var product in cart.CartItems)
                        orderProducts.Add(new OrderProduct(product));

                    order.OrderProducts = orderProducts;
                    order.Completed = true;
                    await orderRepository.UpdateAsync(order);

                    await cartService.DeleteCartAsync(cart);

                    return RedirectToAction(nameof(Completed), new { id = order.Id });
                }
            }
            return RedirectToAction(nameof(HomeController.Index),
                nameof(HomeController).RemoveController());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Finalization([FromRoute] int? id)
        {
            if (id != null)
            {
                var cart = await cartService.GetCartAsync();
                if (cart?.CartItems.Count > 0)
                {
                    var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                    var order = await orderRepository.GetUncompletedOrderAsync(userId, cart.Id);
                    if (order?.AddressesCompleted == false)
                        return RedirectToAction(nameof(Payment), new { order.Id });

                    if (order?.Id != id)
                        return RedirectToAction(nameof(Finalization), new { order.Id });

                    if (order != null)
                        return View(new FinalizationViewModel
                        {
                            Order = order,
                            Cart = cart
                        });
                }
            }
            return RedirectToAction(nameof(HomeController.Index),
                nameof(HomeController).RemoveController());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(AddAddressModel details)
        {
            if (ModelState.IsValid)
            {
                var cart = await cartService.GetCartAsync();
                if (cart?.CartItems.Count > 0)
                {
                    var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                    var order = await orderRepository.GetUncompletedOrderAsync(userId, cart.Id);
                    if (order != null)
                    {
                        order.PaymentOption = details.PaymentMethod;
                        order.Name = details.Name;
                        order.Surname = details.Surname;
                        order.Phone = details.Phone;
                        order.Street = details.Street;
                        order.BuildingNumber = details.BuildingNumber;
                        order.LocalNumber = details.LocalNumber;
                        order.City = details.City;
                        order.PostCode = details.PostCode;
                        order.AddressesCompleted = true;

                        await orderRepository.UpdateAsync(order);

                        return RedirectToAction(nameof(CartController.Finalization), new { order.Id });
                    }
                }
            }
            return RedirectToAction(nameof(CartController.Payment));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Payment([FromRoute] int? id, [FromQuery] bool address = false)
        {
            var cart = await cartService.GetCartAsync();
            if (cart?.CartItems.Count > 0)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value);
                var order = await orderRepository.GetUncompletedOrderAsync(userId, cart.Id);
                if (id == null)
                {
                    if (order != null)
                        return RedirectToAction(nameof(Payment), new { order.Id });

                    var orderId = await orderRepository.AddAsync(new Order
                    {
                        UserId = int.Parse(User.FindFirst(ClaimTypes.Sid).Value),
                        CartId = cart.Id
                    });
                    return RedirectToAction(nameof(Payment), new { Id = orderId });
                }

                var addresses = await accountRepository.FindDeliveryAddressAsync(userId);

                ViewBag.address = address;
                ViewBag.addressExists = addresses == null ? false : true;

                if (order != null)
                {
                    if (address)
                        return View(new AddAddressModel(address ? addresses : null));

                    return View(new AddAddressModel(order));
                }

                return View(new AddAddressModel(address ? addresses : null));
            }

            return RedirectToAction(nameof(HomeController.Index), 
                nameof(HomeController).RemoveController());
        }

        #region ApiMethods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem([FromBody] CartItemModel addItemDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cart = await cartService.GetCartAsync();
                    var cartUpdated = await cartService.AddItemToCartAsync(cart, addItemDetails.ProductId, 
                        addItemDetails.Count);

                    return Ok(new
                    {
                        cartUpdated.CartItems,
                        cartUpdated.SummaryCost
                    });
                }
                catch (DbUpdateException)
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItem([FromBody][Required] int productId)
        {
            if (ModelState.IsValid)
            {
                var cart = await cartService.GetCartAsync();
                var cartUpdated = await cartService.DeleteItemFromCartAsync(cart, productId);

                return Ok(new
                {
                    cartUpdated.CartItems,
                    cartUpdated.SummaryCost
                });
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem([FromBody] CartItemModel details)
        {
            if (ModelState.IsValid)
            {
                var cart = await cartService.GetCartAsync();
                var cartUpdated = await cartService.UpdateItemInCartAsync(cart, 
                    details.ProductId, details.Count);
                return Ok(new
                {
                    cartUpdated.CartItems,
                    cartUpdated.SummaryCost
                });
            }
            return BadRequest();
        }
        #endregion
    }
}
