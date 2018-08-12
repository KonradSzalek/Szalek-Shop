namespace szalkszop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<szalkszop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "szalkszop.Models.ApplicationDbContext";
        }

        protected override void Seed(szalkszop.Models.ApplicationDbContext context)

        {
            //  Dodawanie roli admina

            if (context.Roles.Count() == 0) 
            {
                context.Roles.Add(new IdentityRole { Name = "admin" });
                context.Roles.Add(new IdentityRole { Name = "user" });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var adminEmail = "superuser@email.com";
                var adminPassword = "test00";

                var user = new ApplicationUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Adminuser"
                };

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.AddToRole("e0fe297a - 597d - 4886 - 8433 - 871c127c40e5", "admin");
                var result = userManager.Create(user, adminPassword);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            
        }
    }
}
