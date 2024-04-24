using System.Security.Claims;
using Template.Persistence.Common;
using Microsoft.AspNetCore.Http;
namespace Template.Application;

public class AuditUserProvider : IAuditUserProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	public AuditUserProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}
	
	public long? GetUserId()
	{
		var val = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
		return val != null ? long.Parse(val) : null;
	}

	public string GetUserRole()
	{
		return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? "";
	}
}
