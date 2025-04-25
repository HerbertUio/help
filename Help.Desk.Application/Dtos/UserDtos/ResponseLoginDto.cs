namespace Help.Desk.Application.Dtos.UserDtos;

public class ResponseLoginDto
{
    public DataUserDto User { get; set; }
    public string Token { get; set; }
}