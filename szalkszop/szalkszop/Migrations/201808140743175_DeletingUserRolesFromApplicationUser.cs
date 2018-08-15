namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingUserRolesFromApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" }, "dbo.AspNetUserRoles");
            DropIndex("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" });
            DropColumn("dbo.AspNetUsers", "UserRole_UserId");
            DropColumn("dbo.AspNetUsers", "UserRole_RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRole_RoleId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "UserRole_UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" });
            AddForeignKey("dbo.AspNetUsers", new[] { "UserRole_UserId", "UserRole_RoleId" }, "dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
        }
    }
}
