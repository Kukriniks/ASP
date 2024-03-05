using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.BL
{
	using AutoMapper;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;

	public class ToDoServices : IToDoServices
	{
		private readonly IToDoRepository _todorepository;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public ToDoServices(IToDoRepository toDoRepository, IUserRepository userRepository, IMapper mapper)
		{
			_todorepository = toDoRepository;
			_userRepository = userRepository;
			_mapper			= mapper;
		}

		public ToDoNode AddToDo(CreateToDoDTO node)
		{
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			if (isUserExist != null)
			{
				//var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateToDoDTO, ToDoNode>());
				//var mapper = new Mapper(config);
				//var toDo = mapper.Map<CreateToDoDTO, ToDoNode>(node);
				var toDo = _mapper.Map<CreateToDoDTO, ToDoNode>(node);
				return _todorepository.AddToDo(toDo);
			}
			throw new Exception($"No such user where ID = {node.OwnerId}");
		}

		public ToDoNode UpdateToDo(int id, CreateToDoDTO node)
		{
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			var isToDoIDExist = _todorepository.GetByID(id);

			if (isUserExist != null && isToDoIDExist != null)
			{
				//var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateToDoDTO, ToDoNode>());
				//var mapper = new Mapper(config);
				var toDo = _mapper.Map<CreateToDoDTO, ToDoNode>(node);
				toDo.Id = id;
				var toDoEntity = _todorepository.UpdateToDo(toDo);
				return toDoEntity;
			}
			throw new Exception($"No such ToDo id or User id \n ToDo ID = {id} \n user ID = {node.OwnerId} ");
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

	

		IEnumerable<IToDoNode> IToDoServices.GetList(string? TextPattern, int? offset, int? limit)
		{
			return _todorepository.GetList(TextPattern, offset, limit);
		}
	}
}


