using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Xml.Linq;
using User.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[DisplayName("UserAPI")]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _userService;

		public UserController(IUserServices userService)
		{
			_userService = userService;
		}

		// GET: api/<UserController>
		[HttpGet]
		public IActionResult GetUsers()
		{
			var users = _userService.GetList();
			return Ok(users);
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public IActionResult GetUserByID(int id)
		{
			var user = _userService.GetUserByID(id);
			return Ok(user);
		}

		// POST api/<UserController>
		[HttpPost]
		public IActionResult AddUser([FromBody] UserNode value)
		{
			var user = _userService.AddUser(value);
			return Created($"{user.Id}", user);
		}

		// DELETE api/<UserController>/5
		[HttpDelete()]
		public IActionResult Delete([FromBody]int id)
		{
			var result  = _userService.DeleteUser(id);
			return Ok(result);
		}
	}
}
