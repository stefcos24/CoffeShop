using CoffeShop.Data;
using CoffeShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //IEnumerable<OrderHeader> objList = _db.OrderHeader;

            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetOrderList()
        {
            var orderList = _db.OrderHeader;
            return Json(new { data = _db.OrderHeader });
        }


        #endregion
    }
}
