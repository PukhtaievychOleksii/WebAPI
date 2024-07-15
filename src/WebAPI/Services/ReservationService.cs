

using ErrorOr;

using WebAPI.Domain;

using static Shared;

public class ReservationService : IReservationService
{
    private readonly DatabaseContext _dbContext;

    public ReservationService(DatabaseContext dbContext){
        _dbContext = dbContext;
    }

    public ErrorOr<Created> Create(Reservation reservation)
    {
        User? user = _dbContext.Users.Find(reservation.UserId);
        if(user is null){
            return Error.NotFound("General.NotFound", "User is not found", null);
        }

        Book? book = _dbContext.Books.Find(reservation.BookId);
        if(book is null){
            return Error.NotFound("General.NotFound", "Book is not found", null);
        }

        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
        return Result.Created;
    }

     public ErrorOr<Reservation> Get(Guid reservationId)
    {
        Reservation? reservation = _dbContext.Reservations.Find(reservationId);
        if(reservation is null){
            return Error.NotFound();
        }
        return reservation;
    }

    public ErrorOr<Deleted> Delete(Guid reservationId)
    {
        Reservation? reservation = _dbContext.Reservations.Find(reservationId);
        if(reservation is null){
            return Error.NotFound();
        }

        _dbContext.Reservations.Remove(reservation);
        _dbContext.SaveChanges();
        return Result.Deleted;
    }


    public ErrorOr<UpdatedReservation> Update(Reservation newReservation)
    {
        Reservation? reservation = _dbContext.Reservations.Find(newReservation.Id);
        bool isNewelyCreated = false;

        if(reservation == null){
            _dbContext.Reservations.Add(newReservation);
            isNewelyCreated = true;
        } else{
            _dbContext.Reservations.Remove(reservation);
            _dbContext.Reservations.Add(newReservation);
        }

        _dbContext.SaveChanges();
        return new UpdatedReservation(isNewelyCreated);
    }
}
