namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabaseAndSeedData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        Total = c.Double(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        OrderState = c.Boolean(nullable: false),
                        UserName = c.String(),
                        AdresBasligi = c.String(),
                        Adres = c.String(),
                        Sehir = c.String(),
                        Semt = c.String(),
                        Mahalle = c.String(),
                        PostaKodu = c.String(),
                    })
                .PrimaryKey(t => t.OrderDetailsId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        OrderDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetailsId, cascadeDelete: true)
                .Index(t => t.OrderDetailsId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductNames = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Image = c.String(),
                        IsHome = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Review = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserName = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        MobileNumber = c.Int(nullable: false),
                        Adress = c.String(),
                        City = c.String(),
                        PostCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingId);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        WishlistId = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.WishlistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Orders", "OrderDetailsId", "dbo.OrderDetails");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Orders", new[] { "OrderDetailsId" });
            DropTable("dbo.Wishlists");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.Reviews");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
        }
    }
}
