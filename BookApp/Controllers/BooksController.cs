using BookApp.Data;
using BookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books.ToList();
            return Ok(books); //200 status coode
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBooksWithId([FromRoute(Name = "id")] int id)
        {

            //Bu şekilde de yapılır.
            //var book = ApplicationContext.Books.FirstOrDefault(x => x.Id == id);
            var book = ApplicationContext
                .Books
                .Where(x => x.Id.Equals(id))
                .SingleOrDefault(); // tek bir kayıt ya da null değeri döndür
            if (book == null)
                return NotFound(); //404

            return Ok(book); //200 status code 204 no content
        }

        [HttpPost] //Inmemory çalışır program kapanınca sadece constructorda eklenenler kalır.
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();//400 Code
                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")] //güncelleme //409 conflict
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            // check book
            var entity = ApplicationContext
                .Books
                .Find(x => x.Id == id);
            if (entity is null)
                return NotFound(); //404
            //check id
            if (id != book.Id)
                return BadRequest(); //400
            ApplicationContext.Books.Remove(book);
            book.Id = entity.Id;
            ApplicationContext.Books.Add(book);
            return Ok(book); //200 success
        }

        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext.Books.Find(x => x.Id == id);
            if (entity is null)
                return NotFound(new
                {
                    StatusCode = 404,
                    message = $"Book with id:{id} could not found."
                }); //404 
            ApplicationContext.Books.Remove(entity);
            return NoContent(); //204
        }

        [HttpPatch("{id:int}")] //  nesnenin belli alanları güncellenir.JSONPATCH //415 unsupported media types
        public IActionResult PartialUpdateOneBook([FromRoute(Name ="id")]int id,[FromBody] JsonPatchDocument<Book> book)
        {
            //check entity
            var entity = ApplicationContext.Books.Find(x => x.Id == id);
            if(entity is null)
                return NotFound(); //404
            book.ApplyTo(entity);
            return NoContent(); //204
        }
    }
}
