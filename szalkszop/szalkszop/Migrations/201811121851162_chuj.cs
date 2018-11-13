namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chuj : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            RenameColumn(table: "dbo.ProductImages", name: "ProductId", newName: "Product_Id");
            AlterColumn("dbo.ProductImages", "Product_Id", c => c.Int());
            CreateIndex("dbo.ProductImages", "Product_Id");
            AddForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_Id" });
            AlterColumn("dbo.ProductImages", "Product_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ProductImages", name: "Product_Id", newName: "ProductId");
            CreateIndex("dbo.ProductImages", "ProductId");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
