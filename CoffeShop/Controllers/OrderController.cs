using CoffeShop.Data;
using CoffeShop.Models;
using CoffeShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
            _db.OrderDetail.Include(u => u.Product);
        }

        public IActionResult Index()
        {
            //IEnumerable<OrderHeader> objList = _db.OrderHeader;

            return View();
        }

        public IActionResult Details(int id)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = _db.OrderHeader.FirstOrDefault(u => u.Id == id),
                OrderDetail = _db.OrderDetail.Include(u => u.Product).Where(u => u.OrderHeaderId == id)
            };

            return View(OrderVM);
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
