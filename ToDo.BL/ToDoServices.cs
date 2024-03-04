using ToDo.Repositories;
using ToDo.Models;

namespace ToDo.BL
{
	using System.Collections.Generic;

	public class ToDoServices : IToDoServices
    {
        private readonly IToDoRepository _todorepository;
		private readonly IUserRepository _userRepository;

		public ToDoServices(IToDoRepository toDoRepository, IUserRepository userRepository)
        {
            _todorepository = toDoRepository;
			_userRepository = userRepository;
		}

		public IToDoNode AddToDo(IToDoNode node)
		{
			var isUser = _userRepository.GetUserByID(node.OwnerId);
			if (isUser!=null)
			{
				_todorepository.AddToDo(node);
				return node; 
			}
			throw new Exception($"No such user where ID = {node.OwnerId}");
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
			var isUser = _userRepository.GetUserByID(node.OwnerId);
			if (isUser != null)
			{
				var toDoEntity = _todorepository.UpdateToDo(node);
				if (toDoEntity != null)
				{
					return toDoEntity;
				}
				throw new Exception($"No such ToDo where ID =  {node.Id}");
			}
			throw new Exception($"No such user  where ID = {node.OwnerId}");				
		}

		IEnumerable<IToDoNode> IToDoServices.GetList(string? TextPattern, int? offset, int? limit)
		{
			return _todorepository.GetList(TextPattern, offset, limit);
		}
	}
}


