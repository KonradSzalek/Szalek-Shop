namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chujowsto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductImages", "ImageName", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.ProductImages", "ThumbnailName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.ProductImages", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductImages", "FileName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.ProductImages", "ThumbnailName");
            DropColumn("dbo.ProductImages", "ImageName");
        }
    }
}
