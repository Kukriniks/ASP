
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using ToDo.BL;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ToDoAPI.Controllers
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
        public async Task <IActionResult> GetList(string? nameFreeText, int? offset, int? limit)
        {
            var toDo = await _toDoService.GetListAsync(offset, nameFreeText, limit);
            var count = await _toDoService.CountAsync(nameFreeText);
            HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
            return  Ok(toDo);
        }

        [HttpGet("{id}")]     
        public async Task<IActionResult> GetByID(int id)
        {
            var todo = await _toDoService.GetByIDAsync(id); 
            return Ok(todo);
        }

        [HttpPatch("{id}/IsDone")]
		public async Task<IActionResult> SetAsDone(int id)
        {
            var isDone = await  _toDoService.IsDoneAsync(id);
            return Ok(isDone);

		}

        //POST создать новую запись, вернуть созданную запись с кодом ответа 201 и ссылкой на созданный ресурс.Сохранить время создание записи в UTC формате(DateTime.UtcNow)
        [HttpPost]
        public async Task<IActionResult> AddToDo([FromBody] CreateToDoDTO value)
        {
            var todo = await _toDoService.AddToDoAsync(value);

			return Ok();
        }

        [HttpPut("/UpdateLabl")]
        public async Task<IActionResult> UpdateLabel([FromBody] UpdateToDoLabelDTO labelDTO)
        {
            var node =await _toDoService.UpdateLabelAsync(labelDTO);
           
                return Ok(node);           
        }

        [HttpPut("/Update")]
        public async Task<IActionResult> UpdateToDO([FromBody] UpdateToDoDTO value)
        {
            var node = await _toDoService.UpdateToDoAsync(value);
            return Ok(value);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var result = await _toDoService.DeleteToDoAsync(id);
            return result ? Ok(result) : NotFound(result);
        }
    }
}
