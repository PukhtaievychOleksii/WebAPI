using Microsoft.AspNetCore.Mvc;

using WebAPI.Domain;
[Route("books")]
public class BookController(BookService bookService): ControllerBase
{
    private readonly BookService _bookService = bookService;


    [HttpPost]
    public IActionResult Create([FromBody] CreateBookRequest request){
        //map the request to a domain object 
        Book book = request.ToDomain();

        //invoke an actions on the domain object(services)
        _bookService.Create(book);

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
        Book? book = _bookService.Get(bookId);
        if(book is null){ 
            return Problem($"The book with id {bookId} was not found", null, StatusCodes.Status404NotFound);
        }
        //create a response to an external world
        BookResponse response = BookResponse.FromDomain(book);
        return Ok(response);
    }


    public record CreateBookRequest(string Title, string Author, int Year){
        public Book ToDomain(){
            return new Book(Title, Author, Year);
        }
    };

    public record BookResponse(Guid Id, string Title, string Author, int Year){
        public static BookResponse FromDomain(Book book){
            return new BookResponse(book.Id, book.Title, book.Author, book.Year);
        }
    };
    
}