using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiMongoDB.Models;

namespace WebApiMongoDB
{
	public class BookService
	{
		private readonly IMongoCollection<Book> _books;	
		//constructor
		public BookService(
			IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings
			){ 
				var mongoClient= new MongoClient(bookStoreDatabaseSettings.Value.ConnectionStrings);

			    var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

			_books = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
		
		}

		public async Task<List<Book>> GetBooksAsync()
		{
		return await _books.Find(_ => true).ToListAsync();
		}

		public async Task<Book?> GetBookByIdAsync(string id)
		{
			return await _books.Find(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task CreateBookAsync(Book _newbook) =>
			await _books.InsertOneAsync(_newbook);

		public async Task UpdateBookAsync(string id, Book _updatebook) =>
			await _books.ReplaceOneAsync(x => x.Id == id, _updatebook);

		public async Task RemoveBookAsync(string id) =>
			await _books.DeleteOneAsync(x => x.Id == id);

	}
}
