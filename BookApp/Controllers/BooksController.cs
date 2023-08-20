using BookApp.Models;
using BookApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // consructor üzerinde new DI İfadesi
        private readonly RepositoryContext _repositoryContext;
        public BooksController(RepositoryContext context)
        {
            _repositoryContext = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                //Bu şekilde de yapılır.
                //var book = ApplicationContext.Books.FirstOrDefault(x => x.Id == id);
                var book = _repositoryContext
                    .Books
                    .Where(x => x.Id.Equals(id))
                    .SingleOrDefault(); // tek bir kayıt ya da null değeri döndür
                if (book == null)
                    return NotFound(); //404

                return Ok(book); //200 status code 204 no content
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost] //Inmemory çalışır program kapanınca sadece constructorda eklenenler kalır.
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();//400 Code

                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book); //created
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")] //güncelleme //409 conflict
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                // check book
                var entity = _repositoryContext
                    .Books
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound(); //404

                //check id
                if (id != book.Id)
                    return BadRequest(); //400

                //yeni değerleri maple daha sonra mapper ile
                entity.Title = book.Title;
                entity.Price = book.Price;
                _repositoryContext.SaveChanges();

                return Ok(book); //200 success
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _repositoryContext
                    .Books
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = $"Book with id:{id} could not found."
                    }); //404 

                _repositoryContext.Books.Remove(entity);
                _repositoryContext.SaveChanges();

                return NoContent(); //204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        [HttpPatch("{id:int}")] //  nesnenin belli alanları güncellenir.JSONPATCH //415 unsupported media types
        public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> book)
        {
            try
            {
                //check entity
                var entity = _repositoryContext
                    .Books
                    .Where(x => x.Id == id)
                    .SingleOrDefault();

                if (entity is null)
                    return NotFound(); //404

                book.ApplyTo(entity);
                _repositoryContext.SaveChanges();

                return NoContent(); //204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
