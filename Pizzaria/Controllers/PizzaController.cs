using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Services;

namespace Pizzaria.Controllers
{
    [Route("pizza")]
    public class Pizza : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            PizzaModel pizzaModel = new PizzaModel();
            ViewBag.pizzas = pizzaModel.findAll();
            return View();
        }

       

    }
}