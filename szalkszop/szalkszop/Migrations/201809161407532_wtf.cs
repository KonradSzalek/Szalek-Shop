namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String());
        }
    }
}
