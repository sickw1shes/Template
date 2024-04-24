using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Template.Application.Common;
using Template.Application.Models.User;
using Template.Domain.Constants;
using Template.Domain.Entities;
using Template.Domain.Entities.Identity;
using Template.Persistence;
using Template.Persistence.Common;
using Template.Domain.Entities.Results;
using Template.Application.Responses.Auth;
using Template.Application.Requests.Auth;
using Template.Domain.Configuration;

namespace Template.Application.Services
{

    public class UserAccountManager : UserManager<User>
    {
        private readonly ConfigurationObject _configurationObject;
        private readonly IOptions<JwtConfiguration> _jwtOptions;
        private readonly PostgreDbContext _dbContext;
        private readonly IAuditUserProvider _auditUserProvider;

        public UserAccountManager(
            PostgreDbContext dbContext,
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IOptions<JwtConfiguration> jwtOptions,
            IAuditUserProvider provider,
            ConfigurationObject configurationObject)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                services, logger)
        {
            _jwtOptions = jwtOptions;
            _dbContext = dbContext;
            _auditUserProvider = provider;
            _configurationObject = configurationObject;

        }


        public async Task<Result<IdentityResult>> RegisterAsync(RegisterRequest request)
        {
            var user = Map<User>(request);
            user!.UserProfile = Map<UserProfile>(request);
            user.UserProfile!.Username = request.Username;

            var result = await CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                result = await AddToRoleAsync(user, Roles.User);
            }
            else
            {
                var dictionary = new List<IdentityError>();
                foreach (var error in result.Errors)
                {
                    dictionary.Add(error);
                }

                return new InvalidResult<IdentityResult>(dictionary);
            }

            return new SuccessResult<IdentityResult>(result);
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await FindByNameAsync(request.Username);
            if (user is null || !await CheckPasswordAsync(user, request.Password))
                return new InvalidResult<LoginResponse>("The username or password is incorrect");


            var roles = await GetRolesAsync(user);
            var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, roles.First())
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationObject.Jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configurationObject.Jwt.Issuer,
                audience: _configurationObject.Jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var userInfo = await _dbContext.UserProfiles.FirstAsync(p => p.Id == user.Id, CancellationToken);
            var userData = Map<UserData>(userInfo); // создаем userData из userInfo
            userData!.MapFrom(user); // дозаполняем userData из user
            userData.Role = roles.First();

            var result = new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                UserData = userData
            };

            return new SuccessResult<LoginResponse>(result);
        }
    }
}