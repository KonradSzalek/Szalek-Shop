namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingProductIntValuesToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "AmountInStock", c => c.Int());
            AlterColumn("dbo.Products", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "AmountInStock", c => c.Int(nullable: false));
        }
    }
}
