using Common.Domain;
using Common.Repositories;
using System.Linq.Expressions;
using ToDo.Models;

namespace ToDo.BL
{
	public interface IToDoServices 
	{
		IReadOnlyCollection<ToDoNode> GetList(int? offset, string? nameFreeText, int? limit = 10);
		ToDoNode GetByID(int id);
		ToDoNode AddToDo(CreateToDoDTO node);
		ToDoNode UpdateToDo(int id, CreateToDoDTO node);
		ToDoNode? UpdateLabel(string label, int id);
		bool DeleteToDo(int id);
		object? IsDone(int id);
		int Count(string? nameFreeText);
	}
}