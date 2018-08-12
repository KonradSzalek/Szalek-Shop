namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCategoryList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Name", c => c.String());
            AddColumn("dbo.Products", "Product_Id", c => c.Int());
            AddColumn("dbo.ProductCategories", "ProductCategory_Id", c => c.Byte());
            CreateIndex("dbo.Products", "Product_Id");
            CreateIndex("dbo.ProductCategories", "ProductCategory_Id");
            AddForeignKey("dbo.ProductCategories", "ProductCategory_Id", "dbo.ProductCategories", "Id");
            AddForeignKey("dbo.Products", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.Products", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MyProperty", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ProductCategory_Id" });
            DropIndex("dbo.Products", new[] { "Product_Id" });
            DropColumn("dbo.ProductCategories", "ProductCategory_Id");
            DropColumn("dbo.Products", "Product_Id");
            DropColumn("dbo.Products", "Name");
        }
    }
}
