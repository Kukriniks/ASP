using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.Services;
using Users.BL;
using Users.BL.UserDTO;

namespace User.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IUserServices _userServices;

		public IUserServices UserServices { get; }

		public AuthController(IAuthService authService, IUserServices userServices)
		{
			_authService = authService;
			_userServices = userServices;
		}
		[AllowAnonymous]
		[HttpPost("CreateJwtToken")]
		public async Task<IActionResult> Post(AuthDTO authDTO, CancellationToken cancellationToken)
		{
			var createdUser = await _authService.GetJwtTokenAsync(authDTO, cancellationToken);
			return Ok(createdUser);
		}

		[HttpGet("GetMyInfo")]
		public async Task<IActionResult> GetMyInfo(CancellationToken cancellationToken)
		{
			var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier);
			var user = await _userServices.GetByIDAsync(int.Parse(currentUserId.Value), cancellationToken);
			return Ok(user);
		}
	}
}
