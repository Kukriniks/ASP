

using Common.Domain;

namespace ToDo.BL
{
	public interface IToDoServices 
	{
		IReadOnlyCollection<ToDoNode> GetList(int? offset, string? nameFreeText, int? limit = 10);
		ToDoNode GetByID(int id);
		ToDoNode AddToDo(CreateToDoDTO node);
		ToDoNode UpdateToDo( UpdateToDoDTO node);
		ToDoNode? UpdateLabel(UpdateToDoLabelDTO node);
		bool DeleteToDo(int id);
		bool IsDone(int id);
		int Count(string? nameFreeText);		
	}
}