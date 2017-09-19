using CrudWebApi.Models;

namespace CrudWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrudWebApi.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CrudWebApi.Models.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Users.AddOrUpdate(p => p.Id,
                new User { Name = "Petya" },
                new User { Name = "Vasya" }
            );

            context.Posts.AddOrUpdate(p => p.Id,
                new Post { Title = "First", Content = "Example content 1" },
                new Post { Title = "Second", Content = "Example of 2" }
            );

            context.Categories.AddOrUpdate(p => p.Id,
                new Category { Title = "First" },
                new Category { Title = "Third" }
                );

            context.Reviews.AddOrUpdate(p => p.Id,
                new Review { Content = "Good job" },
                new Review { Content = "Nice post" }
                );
        }
    }
}
