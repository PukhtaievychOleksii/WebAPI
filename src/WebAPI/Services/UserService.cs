

using ErrorOr;

using Microsoft.EntityFrameworkCore;

using static Shared;

public class UserService : IUserService
{
    private readonly DatabaseContext _dbContext;

    public UserService(DatabaseContext dbContext){
        _dbContext = dbContext;
    }


    public ErrorOr<Created> Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<User> Get(Guid userId)
    {
        User? user = _dbContext.Users.Find(userId);
        if(user == null) return Error.NotFound();

        return user;
    }

    public ErrorOr<UpdatedUser> Update(User newUser)
    {
        User? user = _dbContext.Users.Find(newUser.Id);
        bool isNewelyCreated = false;
        if(user == null){
            _dbContext.Users.Add(newUser);
            isNewelyCreated = true;
        } else{
            newUser.SetRegistrationTime(user.RegistrationTime);
            _dbContext.Users.Remove(user);
            _dbContext.Users.Add(newUser);
        }
        _dbContext.SaveChanges();
        return new UpdatedUser(isNewelyCreated);
    }

        public ErrorOr<Deleted> Delete(Guid userId)
    {
        User? user = _dbContext.Users.Find(userId);
        if(user == null) return Error.NotFound();

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        return Result.Deleted;

    }
}