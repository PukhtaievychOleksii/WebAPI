
using ErrorOr;

using static Shared;

public interface IReservationService{
    ErrorOr<Created> Create(Reservation reservation);
    ErrorOr<Reservation> Get(Guid reservationId);
    ErrorOr<Deleted> Delete(Guid reservationId);
    ErrorOr<UpdatedReservation> Update(Reservation newReservation);
}