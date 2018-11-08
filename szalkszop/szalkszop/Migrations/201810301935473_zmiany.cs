namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zmiany : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ImageUploaded1", c => c.Boolean());
            AlterColumn("dbo.Products", "ImageUploaded2", c => c.Boolean());
            AlterColumn("dbo.Products", "ImageUploaded3", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUploaded3", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "ImageUploaded2", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "ImageUploaded1", c => c.Boolean(nullable: false));
        }
    }
}
