using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.BL
{
	using AutoMapper;
	using Common.Repositories;
	using System.Collections.Generic;
	using User.Services;

	public class ToDoServices : IToDoServices
	{
		private static  IRepository<ToDoNode> _todorepository = new BaseRepository<ToDoNode>();
		//private static IRepository<ToDoNode> toDoRepository;
		private readonly IUserServices _userRepository = new UserServices(); //только так получилось внедрить репозиторий пользователей обьявленный в сервисе 
		
		private readonly IMapper _mapper;
		public ToDoServices( IMapper mapper, IUserServices userRepository)
		{
			//_todorepository = toDoRepository;
			_userRepository = userRepository;
			_mapper			= mapper;			
		}
         static ToDoServices()
        {
			//_todorepository = toDoRepository;
			_todorepository.Add(new ToDoNode(1,"first",false,DateTime.UtcNow,DateTime.UtcNow,1));
			_todorepository.Add(new ToDoNode(2, "second", false, DateTime.UtcNow, DateTime.UtcNow, 1));
		}
        public IReadOnlyCollection<ToDoNode> GetList(int? offset, string? nameFreeText, int? limit = 10)
		{
			
			return _todorepository.GetList(
				offset,
				limit,
				nameFreeText == null ? null : n => n.Label.Contains(nameFreeText),
				n => n.Id);
		}

		public ToDoNode AddToDo(CreateToDoDTO node)
		{
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			if (isUserExist != null)
			{
				var toDo = _mapper.Map<CreateToDoDTO, ToDoNode>(node);
				var allList = _todorepository.GetList();
				var maxID = allList.Max(x => x.Id);

				toDo.UpdatedDate = DateTime.UtcNow;
				toDo.CreatedDate = DateTime.UtcNow;
				toDo.Id = maxID + 1;
				return _todorepository.Add(toDo);
			}
			throw new Exception($"No such user where ID = {node.OwnerId}");
		}

		public ToDoNode UpdateToDo(int id, CreateToDoDTO node)
		{
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			var isToDoIDExist = _todorepository.SingleOrDefault(n=>n.Id == id);

			if (isUserExist != null && isToDoIDExist != null)
			{
				var toDo = _mapper.Map<CreateToDoDTO, ToDoNode>(node);
				toDo.Id = id;
				toDo.UpdatedDate = DateTime.UtcNow;
				var toDoEntity = _todorepository.Update(toDo);
				return toDoEntity;
			}
			throw new Exception($"No such ToDo id or User id \n ToDo ID = {id} \n user ID = {node.OwnerId} ");
		}

		public bool DeleteToDo(int id)
		{
			var nodeForDelete = GetByID(id);
			return _todorepository.Delete(nodeForDelete);
		}

		public ToDoNode? GetByID(int id)
		{			
			return _todorepository.SingleOrDefault(n => n.Id == id);
		}

		public object? IsDone(int id)
		{
			var node = GetByID(id);			
			if (node != null)
			{
				node.IsDone = true;
				return new { id = node.Id, isDone = true };
			}
			return null;
		}

		public ToDoNode? UpdateLabel(string label, int id)
		{
			var node = GetByID(id);
			node.Label = label;
			return _todorepository.Update(node);
		}

		public int Count (string? nameFreeText) 
		{ 
			return _todorepository.Count(nameFreeText == null ? null:n=>n.Label.Contains(nameFreeText));
		}

	}
}


