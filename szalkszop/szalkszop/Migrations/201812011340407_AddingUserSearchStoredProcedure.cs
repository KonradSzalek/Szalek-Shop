namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserSearchStoredProcedure : DbMigration
    {
		public override void Up()
		{
			CreateStoredProcedure("SearchUsersStoredProcedure"
			, c => new {
				searchTerm = c.String(),
			},
			@"
			SELECT 
			u.Id,
			u.Name,
			u.Surname,
			u.Email,
			u.RegistrationDateTime
			FROM AspNetUsers AS u 
			inner join (SELECT
			UserId,
			RoleId
			FROM AspNetUserRoles as anur)
			anur on u.Id = anur.UserId
			inner join (SELECT
			Id as [roleId],
			Name
			FROM AspNetRoles as anr)
			anr on anur.RoleId = anr.roleId and anr.Name = 'User' 
 
			WHERE 
			(@searchTerm is NULL OR u.Surname LIKE '%' + @searchTerm + '%') OR
			(@searchTerm is NULL OR u.Email LIKE '%' + @searchTerm + '%')
 
			ORDER BY u.RegistrationDateTime DESC"
			);
		}

		public override void Down()
		{
			DropStoredProcedure("SearchUserssStoredProcedure");
		}
	}
}
