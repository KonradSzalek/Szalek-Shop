namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProductAndProductCategory1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategories", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategory_Id" });
            DropIndex("dbo.Products", new[] { "Product_Id" });
            DropIndex("dbo.ProductCategories", new[] { "ProductCategory_Id" });
            RenameColumn(table: "dbo.Products", name: "ProductCategory_Id", newName: "ProductCategoryId");
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "Product_Id");
            DropColumn("dbo.ProductCategories", "ProductCategory_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "ProductCategory_Id", c => c.Byte());
            AddColumn("dbo.Products", "Product_Id", c => c.Int());
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Byte());
            RenameColumn(table: "dbo.Products", name: "ProductCategoryId", newName: "ProductCategory_Id");
            CreateIndex("dbo.ProductCategories", "ProductCategory_Id");
            CreateIndex("dbo.Products", "Product_Id");
            CreateIndex("dbo.Products", "ProductCategory_Id");
            AddForeignKey("dbo.Products", "ProductCategory_Id", "dbo.ProductCategories", "Id");
            AddForeignKey("dbo.Products", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.ProductCategories", "ProductCategory_Id", "dbo.ProductCategories", "Id");
        }
    }
}
