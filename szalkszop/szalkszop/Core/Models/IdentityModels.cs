using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace szalkszop.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string Surname { get; set; }

		[StringLength(255)]
		public string Address { get; set; }

		[StringLength(100)]
		public string PostalCode { get; set; }

		[StringLength(100)]
		public string City { get; set; }

		[Required]
	    public DateTime RegistrationDateTime { get; set; }

	    public class AuthorizeRedirectToHomePage : AuthorizeAttribute
	    {
		    public override void OnAuthorization(AuthorizationContext filterContext)
		    {
			    if (this.AuthorizeCore(filterContext.HttpContext))
			    {
				    base.OnAuthorization(filterContext);
			    }
			    else
			    {
				    filterContext.Result = new RedirectResult("~/Home/Index");
			    }
		    }
	    }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductsCategories { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<DeliveryType> DeliveryTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrdersItems { get; set; }

		public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()

		{
            return new ApplicationDbContext();
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
		   .HasMany(i => i.Images)
		   .WithRequired()
		   .WillCascadeOnDelete(true);

			base.OnModelCreating(modelBuilder);
		}
	}
}