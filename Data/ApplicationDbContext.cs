using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Areas.Identity.Data;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<OnlineShopUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<SpecialTag> SpecialTags { get; set; }
        public DbSet<Products> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }



    }
}


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<OnlineShopUser>
{
    public void Configure(EntityTypeBuilder<OnlineShopUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
    }
}