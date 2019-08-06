namespace Quiz_August6_KyleElyk.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Quiz_August6_KyleElyk.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Quiz_August6_KyleElyk.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Quiz_August6_KyleElyk.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(x => x.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                manager.Create(new IdentityRole("Admin"));
                manager.Create(new IdentityRole("Manager"));
                manager.Create(new IdentityRole("Customer"));
            }

            if (!context.Users.Any(u => u.UserName == "founder"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "founder" };

                manager.Create(user, "ChangeItAsap!");
                manager.AddToRole(user.Id, "Admin");
            }
            if(!context.Toppings.Any(t=> t.Name == "Pepperoni"))
            {
                context.Toppings.Add(new Topping { Name = "Pepperoni", Price = 1.5 });
                context.Toppings.Add(new Topping { Name = "Peppers", Price = 1.2 });
                context.Toppings.Add(new Topping { Name = "Mushrooms", Price = 1.0 });
                context.Toppings.Add(new Topping { Name = "Ham", Price = 2.1 });
            }
            
        }
    }
}
