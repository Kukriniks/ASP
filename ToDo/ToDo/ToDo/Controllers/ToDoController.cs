
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel;
using ToDo.ToDo.Models;
using ToDo.ToDo.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ToDo.ToDo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[DisplayName("ToDoAPI")]
	public class ToDoController : ControllerBase
	{
		private readonly IToDoServices _toDoService;

		public ToDoController(IToDoServices toDoService)
		{
			_toDoService = toDoService;
		}

		[HttpGet()]
		public IActionResult GetList(string? textPattern, int? offset, int? limit)
		{
			var toDo = _toDoService.GetList(textPattern, offset, limit);
			return Ok(toDo);
		}

		[HttpGet("{id}")]
		//[ApiExplorerSettings(GroupName = "GET")]
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
		public IActionResult AddToDo([FromBody] ToDoNode value)
		{
			try
			{
				var node = _toDoService.AddToDo(value);
				return Created($"/api/ToDo/{node.Id}", node);
			}
			catch (Exception ex)
			{

				return NotFound(ex.Message);
			}
		}

		[HttpPut("{id}, {label}")]
		public IActionResult UpdateLabel(int id, string label)
		{
			var node = _toDoService.UpdateLabel(label, id);
			if (node != null)
			{
				return Ok(node);
			}
			return NotFound();
		}

		[HttpPut("/Update")]
		public IActionResult UpdateToDO([FromBody] ToDoNode value)
		{	
			try
			{
				var node = _toDoService.UpdateToDo(value);

				if (node != null)
				{
					return Ok(node);
				}
			}
			catch (Exception ex)
			{

				return NotFound(ex.Message);
			}
			return NotFound();

		}

		[HttpDelete()]
		public IActionResult Delete([FromBody] int id) 
		{
			var result = _toDoService.DeleteToDo(id);
			return result ? Ok() : NotFound();
		}

	}

}
