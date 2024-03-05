
using ToDo.Models;

namespace ToDo.Repositories
{
	public interface IToDoRepository
	{
		IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit);
		IToDoNode? GetByID(int id);
		ToDoNode AddToDo(ToDoNode node);
		ToDoNode UpdateToDo(ToDoNode node);
		bool DeleteToDo(int id);
		IToDoNode? SetAsDone(int id);
		IToDoNode? UpdateLabel(string label, int id);
	}
}
