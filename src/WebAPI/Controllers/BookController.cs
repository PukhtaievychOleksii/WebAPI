using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Domain;

using static Shared;

[Route("books")]
public class BookController(BookService bookService): ControllerBase
{
    private readonly BookService _bookService = bookService;


    [HttpPost]
    public IActionResult Create([FromBody] BookRequest request){
        Book book = request.ToDomain();

        ErrorOr<Created> serviceResult = _bookService.Create(book);
        if(serviceResult.IsError){
            return Problem("Book was not created successfully ", null, StatusCodes.Status400BadRequest);
        }

        BookResponse response = BookResponse.FromDomain(book);

        return CreatedAtAction(
            nameof(Get),  
            new {bookId = book.Id}, 
            response 
        );
    }

    
    [HttpGet("{bookId:guid}")]
    public IActionResult Get(Guid bookId){
        ErrorOr<Book> serviceResult = _bookService.Get(bookId);
        if(serviceResult.IsError){ 
            return Problem($"The book with id {bookId} doesn't exist", null, StatusCodes.Status404NotFound);
        }
        BookResponse response = BookResponse.FromDomain(serviceResult.Value);
        return Ok(response);
    }


    [HttpDelete("{bookId:guid}")]
    public IActionResult Delete(Guid bookId){
        ErrorOr<Deleted> serviceResult = _bookService.Delete(bookId);
        if(serviceResult.IsError){
            return Problem($"The book with id {bookId} can't be deleted", null, StatusCodes.Status404NotFound);
        }
        return NoContent();
    }


    [HttpPut("{bookId:guid}")]
    public IActionResult Update(Guid bookId, [FromBody] BookRequest request){
        Book newBook = request.ToDomain(bookId);
        ErrorOr<UpdatedBook> serviceResult = _bookService.Update(newBook);

        if(serviceResult.IsError){
            return Problem("The book was not updated", null, StatusCodes.Status400BadRequest);
        }
        
        BookResponse response = BookResponse.FromDomain(newBook);
        if(serviceResult.Value.isNewelyCreated){
            return CreatedAtAction(
                nameof(Get),
                new {bookId = newBook.Id},
                response
            );
        } else {
            return NoContent();
        }
    }
    
}