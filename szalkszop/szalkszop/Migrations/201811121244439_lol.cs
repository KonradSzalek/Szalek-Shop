namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductImages", "FileName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductImages", "FileName", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
