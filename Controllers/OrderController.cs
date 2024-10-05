using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
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
            return View();
        }


        public IActionResult Checkout()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");

            if (products != null)
            {
                foreach (var prd in products)
                {
                    var orderDetails = new OrderDetails();
                    orderDetails.ProductId = prd.Id;

                    order.OrderDetails.Add(orderDetails);
                }
            }

            order.OrderNo = GetOrderNo();
            _db.Orders.Add(order);

            await _db.SaveChangesAsync();

            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }


        public string GetOrderNo()
        {
            int rowCount = _db.Orders.ToList().Count + 1;

            return rowCount.ToString("000");
        }
    }
}
