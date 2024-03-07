
using ToDo.Models;

namespace ToDo.Repositories
{
	public interface IToDoRepository
	{
		IReadOnlyCollection<ToDoNode> GetList(int? offset, string? nameFreeText, int? limit = 10);
		IToDoNode? GetByID(int id);
		ToDoNode AddToDo(ToDoNode node);
		ToDoNode UpdateToDo(ToDoNode node);
		bool DeleteToDo(int id);
		IToDoNode? SetAsDone(int id);
		IToDoNode? UpdateLabel(string label, int id);
	}
}
