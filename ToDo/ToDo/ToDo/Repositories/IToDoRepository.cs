
namespace ToDo.ToDo.Repositories
{
	using Microsoft.AspNetCore.Mvc;
	using ToDo.Models;
	public interface IToDoRepository
    {
		IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit);
        IToDoNode? GetByID(int id);
		IToDoNode AddToDo(IToDoNode node);
        IToDoNode UpdateToDo(IToDoNode node);
        bool DeleteToDo (int id);
		IToDoNode? SetAsDone(int id);
		IToDoNode? UpdateLabel(string label, int id);
	}
}