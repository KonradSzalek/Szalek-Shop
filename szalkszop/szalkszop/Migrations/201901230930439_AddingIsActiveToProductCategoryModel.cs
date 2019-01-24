namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsActiveToProductCategoryModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "IsActive");
        }
    }
}
