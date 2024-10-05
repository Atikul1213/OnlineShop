using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Areas.Identity.Data;
using OnlineShop.Data;

namespace OnlineShop.Areas.Admin.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            var users = _db.onlineShopUsers.ToList();
            return View();
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OnlineShopUser user)
        {
            if (ModelState.IsValid)
            {
                var res = await _userManager.CreateAsync(user, user.PasswordHash);

                if (res.Succeeded)
                {
                    TempData["Success"] = " User Create Successfully ";

                    return RedirectToAction("Index");
                }

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

            }
            return View();
        }
    }
}
