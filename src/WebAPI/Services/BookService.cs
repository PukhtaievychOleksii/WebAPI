
using WebAPI.Domain;

public class BookService
{
    private static readonly List<Book> _books = new List<Book>();
    public void Create(Book book){
        //add book to a database
        _books.Add(book);
    }

    public Book? Get(Guid bookId){
        //fetch book from a database
        Book? result = null;
        foreach(Book book in _books){
            if(book.Id == bookId){
                result = book;
            }

        }
        return result;
        }
}