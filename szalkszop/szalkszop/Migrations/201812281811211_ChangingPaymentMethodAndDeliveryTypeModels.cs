namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPaymentMethodAndDeliveryTypeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryTypes", "Cost", c => c.Double());
            AlterColumn("dbo.PaymentMethods", "Cost", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentMethods", "Cost", c => c.Double(nullable: false));
            AlterColumn("dbo.DeliveryTypes", "Cost", c => c.Double(nullable: false));
        }
    }
}
