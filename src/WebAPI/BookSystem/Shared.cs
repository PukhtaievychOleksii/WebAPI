

using WebAPI.Domain;

public class Shared{
    public record UpdatedBook(bool isNewelyCreated);

    public record UpdatedUser(bool isNewelyCreated);

    public record BookRequest(string Title, string Author, int Year){
        public Book ToDomain(){
            return new Book(Title, Author, Year);
        }

        public Book ToDomain(Guid id){
            return new Book(id, Title, Author, Year);
        }
    };

    public record UserRequest(string Name, string Email){
        public User ToDomain(){
            return new User(Name, Email);
        }

        public User ToDomain(Guid id){
            return new User(id, Name, Email);
        }   
    }

    public record BookResponse(Guid Id, string Title, string Author, int Year){
        public static BookResponse FromDomain(Book book){
            return new BookResponse(book.Id, book.Title, book.Author, book.Year);
        }
    };

    public record UserResponse(Guid id, string Name, string Email, DateTime registrationTime){
        public static UserResponse FromDomain(User user){
            return new UserResponse(user.Id, user.Name, user.Email, user.RegistrationTime);
        }
    }

}