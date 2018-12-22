namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWishlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wishlists", "Quantity", c => c.Int(nullable: false));
            CreateIndex("dbo.Wishlists", "ProductId");
            AddForeignKey("dbo.Wishlists", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlists", "ProductId", "dbo.Products");
            DropIndex("dbo.Wishlists", new[] { "ProductId" });
            DropColumn("dbo.Wishlists", "Quantity");
        }
    }
}
