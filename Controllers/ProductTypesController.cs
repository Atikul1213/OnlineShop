using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }



        // Create Get Action Method
        public ActionResult Create()
        {
            return View();
        }


        // Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductTypes obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "Create Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        // Delete Get Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProductTypes = _db.ProductTypes.Find(id);
            if (ProductTypes == null)
            {
                return NotFound();
            }
            return View(ProductTypes);
        }


        // Delete Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id, ProductTypes obj)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != obj.Id)
            {
                return NotFound();
            }


            var ProductTypes = _db.ProductTypes.Find(id);
            if (ProductTypes == null)
            {
                return NotFound(obj);
            }

            if (ModelState.IsValid)
            {
                _db.Remove(ProductTypes);
                await _db.SaveChangesAsync();
                TempData["success"] = "Delete Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



    }
}
