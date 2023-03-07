namespace WebApiMongoDB.Models
{
	public class BookStoreDatabaseSettings
	{
		public string ConnectionStrings { get; set; } = null!;

		public string DatabaseName { get; set; } = null!;

		public string BooksCollectionName { get; set; } = null!;
	}
}
