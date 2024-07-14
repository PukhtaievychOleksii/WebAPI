
using ErrorOr;

using WebAPI.Domain;

using static Shared;

public interface IBookService
{
    ErrorOr<Created> Create(Book book);
    ErrorOr<Book> Get(Guid bookId);
    ErrorOr<Deleted> Delete(Guid bookId);
    ErrorOr<UpdatedBook> Update(Book newBook);
}   