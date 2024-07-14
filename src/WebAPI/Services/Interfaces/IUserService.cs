
using ErrorOr;

using static Shared;

public interface IUserService
{
    ErrorOr<Created> Create(User user);
    ErrorOr<User> Get(Guid userId);
    ErrorOr<Deleted> Delete(Guid userId);
    ErrorOr<UpdatedUser> Update(User newUser);
}