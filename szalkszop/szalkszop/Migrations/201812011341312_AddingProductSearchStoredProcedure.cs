namespace szalkszop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProductSearchStoredProcedure : DbMigration
    {
		public override void Up()
		{
			CreateStoredProcedure("SearchProductsStoredProcedure"
			, c => new {
				Name = c.String(),
				PriceFrom = c.Double(),
				PriceTo = c.Double(),
				DateTimeFrom = c.DateTime(),
				DateTimeTo = c.DateTime(),
				ProductCategoryId = c.Int()
			},
			@"
			SELECT * FROM Products AS p
			inner join (SELECT
			Id as [categoryid],
			Name as [categoryname]
			FROM ProductCategories as pc)
			pc on p.ProductCategoryId = pc.categoryid
			left join (SELECT 
			Id as [imageid],
			Product_Id,
			ImageName,
			ThumbnailName,
			ROW_NUMBER() OVER(PARTITION BY Product_Id ORDER BY Product_Id) AS [row]
			FROM ProductImages as images)
			images on p.Id = images.Product_Id and [row] = 1
 
			WHERE 
			(@Name is NULL OR p.Name LIKE '%' + @Name + '%') AND 
			(@PriceFrom is NULL OR p.Price >= @PriceFrom) AND 
			(@PriceTo is NULL OR  p.Price <= @PriceTo) AND
			(@DateTimeTo is NULL OR p.DateOfAdding <= @DateTimeTo) AND 
			(@DateTimeFrom is NULL OR p.DateOfAdding >= @DateTimeFrom) AND
			(@ProductCategoryId is NULL OR p.ProductCategoryId = @ProductCategoryId)"
			);
		}

		public override void Down()
		{
			DropStoredProcedure("SearchProductsStoredProcedure");
		}
	}
}
