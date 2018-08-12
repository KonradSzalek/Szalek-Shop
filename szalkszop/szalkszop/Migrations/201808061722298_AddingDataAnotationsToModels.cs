namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnotationsToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ProductCategories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "City", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.ProductCategories", "Name", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
