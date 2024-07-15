

using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using static Shared;
[Route("reservations")]
public class ReservationController(ReservationService reservationService): ControllerBase{
    private readonly ReservationService _reservationService = reservationService;

    [HttpPost]
    public IActionResult CreateReservation([FromBody]ReservationRequest reservationRequest){
        if(reservationRequest is null){
            return Problem("The request is empty,possible problems with userId/bookId", null, StatusCodes.Status400BadRequest);
        } 

        Reservation reservation = reservationRequest.ToDomain();   
        ErrorOr<Created> serviceResult = _reservationService.Create(reservation);
        if(serviceResult.IsError){
            return Problem(serviceResult.Errors.First().Description, null, StatusCodes.Status400BadRequest);
        }
        
        ReservationResponse response = ReservationResponse.FromDomain(reservation);
        return CreatedAtAction(
            nameof(Get),
            new {reservationId = reservation.Id},
            response
        );

    }


    [HttpGet("{reservationId:guid}")]

    public IActionResult Get(Guid reservationId){
        ErrorOr<Reservation> serviceResult = _reservationService.Get(reservationId);
        if(serviceResult.IsError){
            return Problem($"The reservation with id {reservationId} doesn't exist", null, StatusCodes.Status404NotFound);
        }

        ReservationResponse response = ReservationResponse.FromDomain(serviceResult.Value);
        return Ok(response);
    }

    [HttpDelete("{reservationId:guid}")]

    public IActionResult Delete(Guid reservationId){
        ErrorOr<Deleted> serviceResult = _reservationService.Delete(reservationId);
        if(serviceResult.IsError){
            return Problem($"The reservation with id {reservationId} doesn't exist", null, StatusCodes.Status404NotFound);
        }

        return NoContent();
    }

    [HttpPut("{reservationId:guid}")]

    public IActionResult Update(Guid reservationId, [FromBody] ReservationRequest request){
        Reservation reservation = request.ToDomain(reservationId);
        ErrorOr<UpdatedReservation> serviceResult = _reservationService.Update(reservation);
        if(serviceResult.IsError){
            return Problem("The reservation was not updated", null, StatusCodes.Status400BadRequest);
        }

        if(serviceResult.Value.isNewelyCreated){
            return CreatedAtAction(
                nameof(Get),
                new {reservationId = reservationId},
                ReservationResponse.FromDomain(reservation)
            );
        } else {
            return NoContent();
        }
    }
    
}