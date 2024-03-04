using ToDo.ToDo.Models;

namespace ToDo.ToDo.Services
{ 
    public interface IToDoServices
    {      
       IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit);
		IToDoNode GetByID(int id);
		IToDoNode AddToDo(IToDoNode node);
		IToDoNode UpdateToDo(IToDoNode node);
		IToDoNode? UpdateLabel(string label, int id);
		bool DeleteToDo(int id);
		object? IsDone(int id);
	}
}