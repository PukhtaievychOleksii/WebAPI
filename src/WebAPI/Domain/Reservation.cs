
public class Reservation{
    public Guid Id {get; private set;}
    public Guid UserId {get; private set;}
    public Guid BookId {get; private set;}
    public DateTime From {get; private set;}
    public int LengthInDays {get; private set;}
    public bool IsReturned {get; private set;}

    public Reservation( Guid userId, Guid bookId, int lengthInDays, bool isReturned){
        Id = Guid.NewGuid();
        UserId = userId;
        BookId = bookId;
        From = DateTime.Now;
        LengthInDays = lengthInDays;
        IsReturned = isReturned;
    }

    public Reservation(Guid id, Guid userId, Guid bookId, int lengthInDays, bool isReturned){
        Id = id;
        UserId = userId;
        BookId = bookId;
        From = DateTime.Now;
        LengthInDays = lengthInDays;
        IsReturned = isReturned;
    }


}