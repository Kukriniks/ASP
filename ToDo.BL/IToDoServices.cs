using ToDo.Models;

namespace ToDo.BL
{
	public interface IToDoServices
	{
		IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit);
		IToDoNode GetByID(int id);
		ToDoNode AddToDo(CreateToDoDTO node);
		ToDoNode UpdateToDo(int id, CreateToDoDTO node);
		IToDoNode? UpdateLabel(string label, int id);
		bool DeleteToDo(int id);
		object? IsDone(int id);
	}
}