using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Helpers;
using Pizzaria.Models;
using Pizzaria.Services;

namespace Pizzaria.Controllers
{
    
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Pizza.Price * item.Quantity);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            PizzaModel pizzaModel = new PizzaModel();
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Pizza = pizzaModel.find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Pizza = pizzaModel.find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("order")]
        public IActionResult Order()
        {
            if (Request.Cookies["PizzaUser"] == null)
                return RedirectToAction("Index", "User");

            string order = "";
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            foreach (CartItem item in cart)
            {
                order += $"{item.Quantity}: {item.Pizza.Name} - kr. {item.Pizza.Price * item.Quantity} \n";
            }
            string msg = "Following order has just been sent from website\n\n" + order;
            try
            {
                _emailService.SendEmail("per@qwert.dk", "Order: You got work comming", msg);
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            return RedirectToAction("orderconfirmation");
            
        }

        [Route("orderconfirmation")]
        public IActionResult OrderConfirmation()
        {
            
            return View();
        }

        private IEmailService _emailService;
        public CartController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Pizza.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
