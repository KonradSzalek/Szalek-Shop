namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAmountInStockForProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "AmountInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "AmountInStock");
        }
    }
}
