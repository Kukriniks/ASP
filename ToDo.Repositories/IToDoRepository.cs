
using ToDo.Models;

namespace ToDo.Repositories
{ 
		public interface IToDoRepository
		{
		IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit);
		IToDoNode? GetByID(int id);
		IToDoNode AddToDo(IToDoNode node);
		IToDoNode UpdateToDo(IToDoNode node);
		bool DeleteToDo(int id);
		IToDoNode? SetAsDone(int id);
		IToDoNode? UpdateLabel(string label, int id);
		}
}
