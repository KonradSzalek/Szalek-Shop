namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImagesToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUploaded", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "ImageSource");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageSource", c => c.String());
            DropColumn("dbo.Products", "ImageUploaded");
        }
    }
}
