namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartWishlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Wishlists", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.Wishlists", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlists", "UserName", c => c.String());
            DropColumn("dbo.Wishlists", "ProductId");
            DropColumn("dbo.Carts", "ProductId");
            DropColumn("dbo.Carts", "UserId");
        }
    }
}
