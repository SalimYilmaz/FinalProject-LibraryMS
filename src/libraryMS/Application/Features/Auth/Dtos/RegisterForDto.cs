using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Auth.Dtos;
public class UserForRegisterDto<TRegister> : IDto
{
    public TRegister RegisterForDto { get; set; }
    public required string Email { get; set; }
    public required string Password { get; init; }
    

    public UserForRegisterDto()
    {
        Email = string.Empty;
        Password = string.Empty;
        RegisterForDto = default(TRegister)!;
    }
    public UserForRegisterDto(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }
}
