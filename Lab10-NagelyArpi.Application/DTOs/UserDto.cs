namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class UserDto
{
    public UserDto(Guid id, string username, string? email)
    {
        Id = id;
        Username = username;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
}
