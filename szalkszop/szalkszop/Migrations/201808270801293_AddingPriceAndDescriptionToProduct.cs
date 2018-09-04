namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPriceAndDescriptionToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Products", "Price");
        }
    }
}
