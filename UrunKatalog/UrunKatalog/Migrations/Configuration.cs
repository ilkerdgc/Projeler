namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UrunKatalog.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UrunKatalog.Models.DataContext context)
        {
            context.Categories.AddOrUpdate(
                new Models.Categories { CategoryName = "Telefonlar", Description = "En iyi Telefonlar", IsActive = true },
                new Models.Categories { CategoryName = "Bilgisayarlar", Description = "En iyi Bilgisayarlar", IsActive = true },
                new Models.Categories { CategoryName = "Televizyonlar", Description = "En iyi Televizyonlar", IsActive = true }
                );

            context.Products.AddOrUpdate(
                new Models.Products { CategoryId = 1, ProductNames = "Iphone", Description = "Iphone 6s telefon", Image = "1.jpg", Price = 3400, Stock = 4, IsHome = true, IsApproved = true },
                new Models.Products { CategoryId = 1, ProductNames = "Samsung", Description = "Samsung 6s telefon", Image = "2.jpg", Price = 4600, Stock = 12, IsHome = true, IsApproved = true },
                new Models.Products { CategoryId = 2, ProductNames = "Asus", Description = "Asus x550v bilgisayar", Image = "3.jpg", Price = 2200, Stock = 25, IsHome = true, IsApproved = true },
                new Models.Products { CategoryId = 2, ProductNames = "Casper", Description = "Casper xxzc bilgisayar", Image = "4.jpg", Price = 1300, Stock = 8, IsHome = false, IsApproved = true },
                new Models.Products { CategoryId = 3, ProductNames = "LG", Description = "LG fdg televizyon", Image = "5.jpg", Price = 2700, Stock = 10, IsHome = true, IsApproved = true }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
