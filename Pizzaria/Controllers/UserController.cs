using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Newtonsoft.Json;

namespace Pizzaria.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer model)
        {
            try
            {
                saveUserToCookie(model);
                ViewBag.Message = "User @model.Name saved to cookie";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "User could not be saved: Error: " + ex.Message + ".";
                return RedirectToAction(actionName: "Confirmation");
            }

            return RedirectToAction(actionName: "Confirmation");

        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Profile()
        {
            Customer customer = getUserFromCookie();
            if (customer != null)
            {
                return View(customer);
            } else
            {
                return View(); // Return til - du har ikke været her før
            }
            
        }


        [NonAction]
        public void saveUserToCookie(Customer customer)
        {
            var cookieValueJson = JsonConvert.SerializeObject(customer);
            SetCookie("PizzaUser", cookieValueJson, 5);
        }

        [NonAction]
        public Customer getUserFromCookie()
        {
            string customerJson = GetCookie("PizzaUser");
            if(customerJson != null)
            {
                return JsonConvert.DeserializeObject<Customer>(customerJson);
            } else
            {
                return null;
            }
            
        }

        [NonAction]
        private void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }

        [NonAction]
        private string GetCookie(string Key)
        {
            return Request.Cookies[Key];
        }
    }
}