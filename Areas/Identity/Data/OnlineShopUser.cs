using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the OnlineShopUser class
public class OnlineShopUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}

