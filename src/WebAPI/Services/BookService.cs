
using WebAPI.Domain;
using ErrorOr;
using static Shared;

public class BookService: IBookService
{
    private readonly DatabaseContext _dbContext;

    public BookService(DatabaseContext dbContext){
        _dbContext = dbContext;
    }

    public ErrorOr<Created> Create(Book book){
        //add book to a database
        //_books.Add(book);
        _dbContext.Books.Add(book);  
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Book> Get(Guid bookId){
     

        Book? book = _dbContext.Books.Find(bookId);
        if(book is null){
            return Error.NotFound();
        }
        return book;
    }


    public ErrorOr<UpdatedBook> Update(Book newBook){
        Book? book = _dbContext.Books.Find(newBook.Id);
        bool isNewelyCreated  = book is null;

        if(isNewelyCreated){
            _dbContext.Books.Add(newBook);
        } else{
            _dbContext.Books.Remove(book);
            _dbContext.Books.Add(newBook);
            //_dbContext.Books.Update(newBook);
        }

        _dbContext.SaveChanges();
        return new UpdatedBook(isNewelyCreated);


    }

    public ErrorOr<Deleted> Delete(Guid bookId){
        Book? book = _dbContext.Books.Find(bookId);
        if(book is null){
            return Error.NotFound();
        }
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
        return Result.Deleted;
    }




}