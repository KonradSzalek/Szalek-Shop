namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAmountOfProductsForProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "AmountOfProducts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "AmountOfProducts");
        }
    }
}
