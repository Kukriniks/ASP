
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
        public IActionResult GetList(string? nameFreeText, int? offset, int? limit)
        {
            var toDo = _toDoService.GetList(offset, nameFreeText, limit);
            var count = _toDoService.Count(nameFreeText);
            HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
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
        public IActionResult AddToDo([FromBody] CreateToDoDTO value)
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

        [HttpPut("/UpdateLabl")]
        public IActionResult UpdateLabel([FromBody] UpdateToDoLabelDTO lablDTO)
        {
            var node = _toDoService.UpdateLabel(lablDTO);
            if (node != null)
            {
                return Ok(node);
            }
            return NotFound();
        }

        [HttpPut("/Update")]
        public IActionResult UpdateToDO([FromBody] UpdateToDoDTO value)
        {
            var node = _toDoService.UpdateToDo(value);
            return Ok(value);
        }

        [HttpDelete()]
        public IActionResult Delete([FromBody] int id)
        {
            var result = _toDoService.DeleteToDo(id);
            return result ? Ok() : NotFound();
        }
    }
}
