namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PaymentMethodId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "TotalPrice", c => c.Double(nullable: false));
            CreateIndex("dbo.Orders", "DeliveryTypeId");
            CreateIndex("dbo.Orders", "PaymentMethodId");
            AddForeignKey("dbo.Orders", "DeliveryTypeId", "dbo.DeliveryTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Orders", "DeliveryTypeId", "dbo.DeliveryTypes");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            DropIndex("dbo.Orders", new[] { "DeliveryTypeId" });
            DropColumn("dbo.Orders", "TotalPrice");
            DropColumn("dbo.Orders", "PaymentMethodId");
            DropColumn("dbo.Orders", "DeliveryTypeId");
        }
    }
}
