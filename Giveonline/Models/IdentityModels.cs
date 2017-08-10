using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Giveonline.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
    }




    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<SizeProduct> SizeProducts { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }






        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


}