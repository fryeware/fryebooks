namespace Fryebooks.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Fryebooks.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Fryebooks.Models.ApplicationDbContext context)
        {
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
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Download" });
            context.Roles.AddOrUpdate(
                 r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Admin" });
        }
    }
}
