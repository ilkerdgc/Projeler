namespace UrunKatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderState : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "OrderState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "OrderState", c => c.Boolean(nullable: false));
        }
    }
}
