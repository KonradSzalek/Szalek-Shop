namespace szalkszop.DTO
{
	public class ApiProductDto
	{
		public string Name { get; set; }

		public string CategoryName { get; set; }

		public int AmountInStock { get; set; }

		public string DateOfAdding { get; set; }

		public double Price { get; set; }

		public string Description { get; set; }

		public string EditLink { get; set; }

		public string DeleteLink { get; set; }
	}
}