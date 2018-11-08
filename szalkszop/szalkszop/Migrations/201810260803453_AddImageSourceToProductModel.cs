namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageSourceToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageSource");
        }
    }
}
