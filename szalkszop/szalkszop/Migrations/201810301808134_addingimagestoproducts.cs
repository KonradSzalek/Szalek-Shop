namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingimagestoproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUploaded1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "ImageUploaded2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "ImageUploaded3", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "ImageUploaded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageUploaded", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "ImageUploaded3");
            DropColumn("dbo.Products", "ImageUploaded2");
            DropColumn("dbo.Products", "ImageUploaded1");
        }
    }
}
