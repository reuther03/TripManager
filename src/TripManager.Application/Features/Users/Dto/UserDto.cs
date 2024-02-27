using TripManager.Domain.Users;

namespace TripManager.Application.Features.Users.Dto;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public string Fullname { get; init; }

    public static UserDto AsDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Fullname = user.Fullname
        };
    }
}