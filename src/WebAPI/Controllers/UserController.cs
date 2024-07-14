

using ErrorOr;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using static Shared;

[Route("users")]
public class UserController(UserService userService): ControllerBase{
    private readonly UserService _userService = userService;

    [HttpPost]
    public IActionResult Create([FromBody]UserRequest request){
        User user = request.ToDomain();

        ErrorOr<Created> serviceResult = _userService.Create(user);

        if(serviceResult.IsError){
        return Problem("User was not created successfully", null, StatusCodes.Status400BadRequest);
        }

        UserResponse response = UserResponse.FromDomain(user);
        return CreatedAtAction(
            nameof(Get),
            new {userId = user.Id},
            response
        );
    }


    [HttpGet("{userId:guid}")]
    public IActionResult Get(Guid userId){
        ErrorOr<User> serviceResult = _userService.Get(userId);

        if(serviceResult.IsError){
            return Problem($"User with id{userId} can't be found",null, StatusCodes.Status404NotFound);
        }

        UserResponse response = UserResponse.FromDomain(serviceResult.Value);
        return Ok(response);
    }


    [HttpDelete("{userId:guid}")]
    public IActionResult Delete(Guid userId){
        ErrorOr<Deleted> serviceResult = _userService.Delete(userId);
        
        if(serviceResult.IsError){
            return Problem($"User with Id {userId} doesn't exist",null,StatusCodes.Status404NotFound);
        }

        return NoContent();
    }


    [HttpPut("{userId:guid}")]
    public IActionResult Update(Guid userId,[FromBody] UserRequest request){
        User newUser = request.ToDomain(userId);

        ErrorOr<UpdatedUser> serviceResult = _userService.Update(newUser);
        if(serviceResult.IsError){
            return Problem("User can't be Updated",null,StatusCodes.Status400BadRequest);
        }

        bool isNewelyCreated = serviceResult.Value.isNewelyCreated;
        if(isNewelyCreated){
            UserResponse response = UserResponse.FromDomain(newUser);
            return CreatedAtAction(
                nameof(Get),
                new {userId = newUser.Id},
                response
            );
        } else{
            return NoContent();
        }

    }
}