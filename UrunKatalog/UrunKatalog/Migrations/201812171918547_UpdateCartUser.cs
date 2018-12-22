namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Carts", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "UserId", c => c.Int(nullable: false));
        }
    }
}
