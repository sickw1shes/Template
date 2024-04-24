using Template.Application.Requests.Auth;
using Template.Application.Responses.Auth;
using Template.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Template.Domain.Entities.Results;

namespace Template.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserAccountManager _userAccountManager;

        public AuthController(UserAccountManager userAccountManager)
        {
            _userAccountManager = userAccountManager;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<Result<IdentityResult>> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _userAccountManager.RegisterAsync(request);
            return result;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<Result<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _userAccountManager.LoginAsync(request);
            return result;
        }

        [HttpGet]
        [Route("check"), Authorize]
        public async Task<Result<bool>> Check()
        {
            return new SuccessResult<bool>(true);
        }
    }
}
