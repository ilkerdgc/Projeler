namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCart : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductId" });
        }
    }
}
