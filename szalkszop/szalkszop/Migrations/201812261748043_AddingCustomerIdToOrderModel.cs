namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCustomerIdToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "SetOrderStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "SetOrderStatus", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}
