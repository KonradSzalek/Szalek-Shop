namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WholeProjectMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String(maxLength: 100));
            DropColumn("dbo.ProductCategories", "AmountOfProducts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "AmountOfProducts", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String());
            DropTable("dbo.ProductImages");
        }
    }
}
