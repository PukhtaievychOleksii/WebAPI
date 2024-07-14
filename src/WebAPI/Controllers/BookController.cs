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
        //map the request to a domain object 
        Book book = request.ToDomain();

        //invoke an actions on the domain object(services)
        ErrorOr<Created> serviceResult = _bookService.Create(book);
        if(serviceResult.IsError){
            return Problem("Book was not created successfully ", null, StatusCodes.Status400BadRequest);
        }

        //create a response from the domain object(back to external representation)
        BookResponse response = BookResponse.FromDomain(book);

        return CreatedAtAction(
            nameof(Get),  //method to fetch a created resource
            new {bookId = book.Id}, // parameters needed to call the method
            response //response itself
        );
    }

    
    [HttpGet("{bookId:guid}")]
    public IActionResult Get(Guid bookId){
        //invoke the service
        ErrorOr<Book> serviceResult = _bookService.Get(bookId);
        if(serviceResult.IsError){ 
            return Problem($"The book with id {bookId} doesn't exist", null, StatusCodes.Status404NotFound);
        }
        //create a response to an external world
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