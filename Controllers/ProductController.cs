using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
        }

        //Post Index Action Method
        [HttpPost]
        public IActionResult Index(int? lowPrice, int? highPrice)
        {
            var product = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).Where(c => c.Price >= lowPrice && c.Price <= highPrice).ToList();

            if(lowPrice==null || highPrice==null)
            {
                product = _db.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList();
            }
            return View(product);
        }



        // Get Create Method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        // Post Create Method
        [HttpPost]

        public async Task<IActionResult> Create(Products products)
        {

            

            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product already exixt";
                    ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
                    return View();
                }
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(products);
        }




        //Get Edit Action Method

        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
            if(id==null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag).FirstOrDefault(c=>c.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Post Edit Action Method
        [HttpPost]

        public async Task<IActionResult> Edit(Products product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }














        // Get Delete Method
        public IActionResult Delete(int? id)
        {
           
           
            if(id==null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c=>c.SpecialTag).Include(c=>c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Post Delete Method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {

            if(id==null)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
             
        }






    }
}
