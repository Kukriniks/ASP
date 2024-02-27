
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ToDoController : ControllerBase
	{
		#region get 

		//GET /todos - получить все записи. Опционально принимать GET параметры limit (int), offset (int). Limit - максимально количество возвращаемых записей, offset - количество пропускаемых записей 
		[HttpGet]
		public List<ToDoNode> Get()
		{
			return ToDoList.ToDoLists;
		}

		//GET /todos/{id} - получить запись по Id
		[HttpGet("{id}")]
		[ProducesResponseType<ToDoNode>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<ToDoNode> Get(int id)
		{
			var todo = ToDoList.ToDoLists.Find(i => i.Id == id);
			return todo == null ? NotFound() : todo;
		}

		//GET /todos - получить все записи. Опционально принимать GET параметры limit (int), offset (int). Limit - максимально количество возвращаемых записей, offset - количество пропускаемых записей 
		[HttpGet("Limit={limit};Offset={offset}")]
		[ProducesResponseType<ToDoNode>(StatusCodes.Status200OK)]
		public ActionResult<List<ToDoNode>> Get(int limit, int offset)
		{
			var todo = ToDoList.ToDoLists.Skip(offset).Take(limit).ToList();
			return todo == null ? NotFound() : todo;
		}

		//GET /todos/{id}/IsDone - получить флаг (вернуть json вида {id:1, IsDone: true})
		[HttpGet("/ToDo/{id}/IsDone")]
		[ProducesResponseType<ToDoNode>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public object GetFlag(int id)
		{			
			var todo = ToDoList.ToDoLists.Find(i => i.Id == id);
			if (todo == null) 
			{
				return NotFound(); 
			}
			else
			{
				var forSend = new { Id = id, IsDone = todo.IsDone };	
				return forSend;
			}		
		}
		#endregion

		//POST  создать новую запись, вернуть созданную запись с кодом ответа 201 и ссылкой на созданный ресурс.Сохранить время создание записи в UTC формате(DateTime.UtcNow)
		[HttpPost]
		[ProducesResponseType<ToDoNode>(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Post([FromBody] ToDoNode value)
		{
			var time = DateTime.Now;
			int id = 1;
			if (ToDoList.ToDoLists.Count > 0)
			{
				id = ToDoList.ToDoLists.Select(l => l.Id).Max() + 1;
			}
			ToDoNode node = new ToDoNode(id, value.Label, value.IsDone, createdDate: time, updatedDate: time);
			ToDoList.ToDoLists.Add(node);			
			return Created($"/api/ToDo/{id}", node); //Спросить !
		}

		// DELETE /todos/{id} - удалить запись 
		[ProducesResponseType<ToDoNode>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpDelete("{id}")]
		public ActionResult<ToDoNode> DELETE(int id)
		{
			var todo = ToDoList.ToDoLists.Find(i => i.Id == id);
			if (todo == null)
			{
				return NotFound();
			}
			ToDoList.ToDoLists.Remove(todo);
			return todo;
		}

		//PUT /todos/{id} - обновить запись, вернуть обновленную запись. Обновить поле UpdatedDate текущим UTC временем. Не обновлять поля CreatedDate и UpdatedDate данными от клиентской стороны 
		[HttpPut("{id}")]
		public ActionResult<ToDoNode> Put(int id, [FromBody] string value)
		{
			var index = ToDoList.ToDoLists.FindIndex(i => i.Id == id);
			if (index == -1)
			{
				return NotFound();
			}
			else
			{
				ToDoList.ToDoLists[index].UpdatedDate = DateTime.UtcNow;
				ToDoList.ToDoLists[index].Label = value;
				return ToDoList.ToDoLists[index];
			}
		}

		//PATCH /todos/{id}/IsDone - обновить поле IsDone у конкретной записи, запрос отправляет json вида {isDone:true}, ответ в виде {id:1, IsDone: true}		 
		[HttpPatch("{id}/IsDone")]
		public object Patch(int id, [FromBody] JsonPatchDocument<ToDoNode> putch)
		{
			var index = ToDoList.ToDoLists.FindIndex(i => i.Id == id);
			if (index == -1)
			{
				return NotFound();
			}
			else
			{
				putch.ApplyTo(ToDoList.ToDoLists[index]);			
			}
			var forSend = new { Id = id, IsDone = ToDoList.ToDoLists[index].IsDone };
			return forSend;
		}
		/*такое тело запроса для применения патча
		 * видимо не совсем то что требуется в задании (
		 * [
			  {
				"op": "replace",
				"path": "/isDone",
				"value": "true"
			  }
			]
		 */
	}
}
