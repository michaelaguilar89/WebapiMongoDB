using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApiMongoDB.Models;
using WebApiProduccion.Models;

namespace WebApiMongoDB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookStoreApiController : ControllerBase
	{
		private readonly BookService _bookService;
		private readonly myResponse _response;

		public BookStoreApiController(BookService bookService)
		{
			_bookService=bookService;
			_response = new myResponse();
		}
		[HttpGet]
		public async Task<IActionResult> getBooks()
		{
			try
			{
				var list = await _bookService.GetBooksAsync();
				
				_response.Result = list;
				return Ok(_response);
			}
			catch (Exception e)
			{
				_response.ErrorMessages = new List<string> {e.Message };
				return BadRequest(_response);
			}
		}

		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult> getBookById(string id)
		{
			try
			{
				var book = await _bookService.GetBookByIdAsync(id);
				if (book==null)
				{
					_response.DisplayMessage = "Record id : " + id + " not found";
					return BadRequest(_response);
				}
				
				_response.Result = book;
				return Ok(_response);
			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				return BadRequest(_response);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Create(Book book)
		{
			try
			{
				await _bookService.CreateBookAsync(book);
				_response.DisplayMessage = "new record created";
				return Ok(_response);
			}
			catch (Exception e)
			{
				_response.ErrorMessages = new List<string> { e.Message };
				return BadRequest(_response);
			}
		}

		[HttpPut("{id:length(24)}")]
		public async Task<ActionResult> Update(string id,Book book)
		{
			try
			{
				var mybook = await _bookService.GetBookByIdAsync(id);
				if (mybook==null)
				{
					_response.DisplayMessage = "Record id : " + id + " not found";
					return BadRequest(_response);
				}
				await _bookService.UpdateBookAsync(id, book);
				_response.DisplayMessage = "Record id : " + id + " has update";
				return Ok(_response);	
			}
			catch (Exception e)
			{
				_response.ErrorMessages = new List<string> { e.Message };
				return BadRequest(_response);
			}
		}

		[HttpDelete("{id:length(24)}")]
		public async Task<ActionResult> Delete(string id)
		{
			try
			{
				var book = await _bookService.GetBookByIdAsync(id);
				if (book==null)
				{
					_response.DisplayMessage = "Record id : " + id + " not found";
					return BadRequest(_response);

				}
				await _bookService.RemoveBookAsync(id);
				_response.DisplayMessage = "Record id : " + id + " has removed";
				return Ok(_response);

			}
			catch (Exception e)
			{
				_response.ErrorMessages = new List<string> { e.Message };
				return BadRequest(_response);
			}
		}
	}
}
