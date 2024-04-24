using Template.Application.Models.User;

namespace Template.Application.Responses.Auth;

public sealed class LoginResponse
{
	public string Token { get; set; } = string.Empty;
	public UserData UserData { get; set; }
}
