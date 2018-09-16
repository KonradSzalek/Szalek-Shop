namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductCategories", "AmountOfProducts");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductCategories", "AmountOfProducts", c => c.Int(nullable: false));
        }
    }
}
