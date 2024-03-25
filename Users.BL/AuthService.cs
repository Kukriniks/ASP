using Common.Repositories;
using System.Security.Claims;
using Users.BL.UserDTO;
using Common.BL.Exceptions;
using Common.Auth.Application.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Users.BL
{
	public class AuthService : IAuthService
	{
		private readonly IBaseRepository<Common.Domain.User> _userRepository;
		private readonly IConfiguration _configuration;

		public AuthService(IBaseRepository<Common.Domain.User> user, IConfiguration configuration)
		{
			_userRepository = user;
			_configuration = configuration;
		}

		public async Task<string> GetJwtTokenAsync(AuthDTO authDTO, CancellationToken cancellationToken)
		{
			var user = await _userRepository.SingleOrDefaultAsync(u => u.Login == authDTO.Login.Trim(), cancellationToken);

			if (user is not null)
			{
				throw new NotFoundException($" There is no user {authDTO.Login}");
			}

			if (!PasswordHasher.VerifyPassword(authDTO.Password, user.PasswordHash))
			{
				throw new ForbiddenException();
			}

			var claims = new List<Claim>
			{
				new (ClaimTypes.Name, authDTO.Login),

				new (ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var secureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
			var credentials = new SigningCredentials(secureKey, SecurityAlgorithms.Sha256);

			var tokenDescription = new JwtSecurityToken(_configuration["Jwt:Issuer"]!, _configuration["Jwt:Audience"]!, claims,
				expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: credentials);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);

			return token;
		}
	}
}
