using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using User.Services;
using Users.BL.UserDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.API.Controllers
{
	[Route("/api[controller]")]
	[ApiController]
	[DisplayName("UserAPI")]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userService;

		public UserController(IUserServices userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList(string? nameFreeText, int? offset, int? limit)
		{
			var toDo = await _userService.GetListAsync(offset, nameFreeText, limit);
			return Ok(toDo);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByID(int id)
		{
			var todo = await _userService.GetByIDAsync(id);
			return Ok(todo);
		}

		[HttpPost("/AddUser")]
		public async Task<IActionResult> AddUser(CancellationToken cancellationToken,[FromBody] CreateUserDTO value)
		{
			var user = await _userService.AddUserAsync( value, cancellationToken);
			return Ok(user);
		}

		[HttpDelete()]
		public async Task<IActionResult> Delete([FromBody] int id, CancellationToken cancellationToken)
		{
			var result = await _userService.DeleteUserAsync(id);
			return Ok(result);
		}


	}
}
