
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using ToDo.ToDo.Domain;
using ToDo.ToDo.Models;
using ToDo.ToDo.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ToDo.ToDo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		private readonly IToDoServices _toDoService;

		public ToDoController(IToDoServices toDoService)
		{
			_toDoService = toDoService;
		}
	
		[HttpGet]
		public IActionResult GetList(string? textPattern, int? offset, int? limit)
		{
			var toDo = _toDoService.GetList(textPattern, offset, limit);
			return Ok(toDo);
		}

		[HttpGet("{id}")]
		public IActionResult GetByID(int id)
		{
			var todo = _toDoService.GetByID(id);
			if (todo == null)
			{
				NotFound($"/{id}");
			}
			return Ok(todo);
		}

		[HttpPatch("{id}/IsDone")]
		public object SetAsDone(int id)
		{
			var isDone = _toDoService.IsDone(id);
			if (isDone != null)
			{
				return isDone;
			}
			else
			{
				return NotFound();
			}
		}

		//POST создать новую запись, вернуть созданную запись с кодом ответа 201 и ссылкой на созданный ресурс.Сохранить время создание записи в UTC формате(DateTime.UtcNow)
		[HttpPost]
		public IActionResult Post([FromBody] ToDoNode value)
		{
			var node = _toDoService.AddToDo(value);
			return Created($"/api/ToDo/{node.Id}", node); //Спросить !
		}

		[HttpPut("{id}, {label}")]
		public IActionResult Put(int id, string label)
		{
			var node = _toDoService.UpdateLabel(label, id);
			if (node != null)
			{
				return Ok(node);
			}
			return NotFound();
		}

	}

}
