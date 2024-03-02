namespace ToDo.ToDo.Services
{
    using ToDo.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToDo.Models;

    public class ToDoServices : IToDoServices
    {
        private readonly IToDoRepository _todorepository;
        public ToDoServices(IToDoRepository toDoRepository)
        {
            _todorepository = toDoRepository;
        }

		public IToDoNode AddToDo(IToDoNode node)
		{
			_todorepository.AddToDo(node);
            return node;
		}

		public bool DeleteToDo(int id)
		{
			return _todorepository.DeleteToDo(id);
		}

		public IToDoNode? GetByID(int id)
		{
			return _todorepository.GetByID(id);
		}		

		public object? IsDone(int id)
		{
			var node = _todorepository.SetAsDone(id);
			if (node != null)
			{
				return new { id = node.Id, isDone = true };
			}
			return null; 
		}

		public IToDoNode? UpdateLabel(string label, int id)
		{
			return _todorepository.UpdateLabel(label, id);
		}

		public IToDoNode UpdateToDo(IToDoNode node)
		{
			return _todorepository.UpdateToDo(node);
		}

		IEnumerable<IToDoNode> IToDoServices.GetList(string? TextPattern, int? offset, int? limit)
		{
			return _todorepository.GetList(TextPattern, offset, limit);
		}
	}
}


