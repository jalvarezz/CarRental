namespace CarRental.Web.Migrations
{
    using CarRental.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarRental.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CarRental.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(CarRental.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //By default the @ is not allowed. Fixed with this line
            manager.UserValidator = new UserValidator<ApplicationUser>(manager) { AllowOnlyAlphanumericUserNames = false };

            if (manager.FindByName("juan.alvarezz@gmail.com") == null)
            {
                manager.Create(new ApplicationUser
                {
                    UserName = "juan.alvarezz@gmail.com",
                    FirstName = "Juan",
                    LastName = "Alvarez",
                    Email = "juan.alvarezz@gmail.com"
                }, "B3nd1t4cl4v3");
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
