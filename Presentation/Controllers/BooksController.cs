using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController] //http 400 ve binding 
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        // consructor üzerinde new DI İfadesi
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
            
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
                //Bu şekilde de yapılır.
                //var book = ApplicationContext.Books.FirstOrDefault(x => x.Id == id);
                var book = _manager
                    .BookService.GetOneBookById(id, false);

                if (book == null)
                    throw  new BookNotFoundException(id); //404

                return Ok(book); //200 status code 204 no content
            
        }

        [HttpPost] //Inmemory çalışır program kapanınca sadece constructorda eklenenler kalır.
        public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
        {
                if (bookDto is null)
                    return BadRequest();//400 Code

                if (!ModelState.IsValid)
                     return UnprocessableEntity(ModelState); //422

                var book =_manager.BookService.CreateOneBook(bookDto);

                return StatusCode(201, book); //created
            
        }

        [HttpPut("{id:int}")] //güncelleme //409 conflict
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, 
            [FromBody] BookDtoForUpdate bookDto)
        {
                if (bookDto is null)
                    return BadRequest();//400 Code

                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState); //422

                _manager.BookService.UpdateOneBook(id, bookDto, false);
                return NoContent(); //204

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
                _manager.BookService.DeleteOneBook(id, false);

                return NoContent(); //204
          

        }

        [HttpPatch("{id:int}")] //  nesnenin belli alanları güncellenir.JSONPATCH //415 unsupported media types
        public IActionResult PartialUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {
                if(bookPatch is null)
                    return BadRequest();

                var result = _manager.BookService.GetOneForPatch(id, false);

                bookPatch.ApplyTo(result.bookDtoForUpdate,ModelState);
                TryValidateModel(result.bookDtoForUpdate);

                if(!ModelState.IsValid)
                    return UnprocessableEntity();

                 _manager.BookService.SaveChangesForPatch(result.bookDtoForUpdate, result.book);

                return NoContent(); //204
           
        }
    }
}
