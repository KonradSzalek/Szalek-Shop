using System;
using szalkszop.Core.Models;

namespace szalkszop.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "szalkszop.Models.ApplicationDbContext";
		}

		protected override void Seed(ApplicationDbContext context)

		{
			//  Adding Admin Role to superuser

			if (!context.Roles.Any())
			{
				context.Roles.Add(new IdentityRole { Name = "Admin" });
				context.Roles.Add(new IdentityRole { Name = "User" });
				context.SaveChanges();
			}

			if (!context.Users.Any())
			{
				var adminPassword = "Admin123#";

				var user = new ApplicationUser
				{
					UserName = "superuser@email.com",
					Email = "superuser@email.com",
					Name = "Admin",
					Surname = "Admin",
					Address = "Address",
					PostalCode = "1111",
					City = "City",
					RegistrationDateTime = DateTime.Now,
				};
				var store = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(store);

				userManager.Create(user, adminPassword);
				userManager.AddToRole(user.Id, "Admin");
			}
		}
	}
}
