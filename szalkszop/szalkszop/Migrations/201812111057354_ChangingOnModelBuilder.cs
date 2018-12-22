namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingOnModelBuilder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_Id" });
            AlterColumn("dbo.ProductImages", "Product_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductImages", "Product_Id");
            AddForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_Id" });
            AlterColumn("dbo.ProductImages", "Product_Id", c => c.Int());
            CreateIndex("dbo.ProductImages", "Product_Id");
            AddForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products", "Id");
        }
    }
}
