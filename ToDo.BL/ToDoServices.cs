using ToDo.Models;
namespace ToDo.BL
{
	using AutoMapper;
	using Common.Repositories;
	using FluentValidation;
	using System.Collections.Generic;
	using ToDo.BL.Validators;
	using User.Services;
	using ToDo.BL.Mapping;

	public class ToDoServices : IToDoServices
	{
		private static  IBaseRepository<ToDoNode> _todorepository = new BaseRepository<ToDoNode>();	
		private readonly IUserServices _userRepository = new UserServices(); //только так получилось внедрить репозиторий пользователей обьявленный в сервисе 
		
		private readonly IValidator<CreateToDoDTO> _validator;
		private readonly IMapper _mapper;
		
		public ToDoServices(IMapper mapper, IUserServices userRepository, IValidator<CreateToDoDTO> validator)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_validator = validator;


			_todorepository.Add(new ToDoNode(1, "first", false, DateTime.UtcNow, DateTime.UtcNow, 1));
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
			
			var validatorResult = _validator.Validate(node);
			if (!validatorResult.IsValid) 
			{
				throw new Exception($"Incorrect Data");
			}
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			if (isUserExist != null)
			{
				var toDo = _mapper.Map<CreateToDoDTO, ToDoNode>(node);
				var allList = _todorepository.GetList();
				int maxID = 0;
				try
				{
					 maxID = allList.Max(x => x.Id);
				}
				catch (Exception)
				{
					throw;
				}				

				toDo.UpdatedDate = DateTime.UtcNow;
				toDo.CreatedDate = DateTime.UtcNow;
				toDo.Id = maxID + 1;
				return _todorepository.Add(toDo);
			}
			throw new Exception($"No such user where ID = {node.OwnerId}");
		}

		public ToDoNode UpdateToDo(UpdateToDoDTO node)
		{
			var isUserExist = _userRepository.GetUserByID(node.OwnerId);
			var isToDoIDExist = _todorepository.SingleOrDefault(n=>n.Id == node.ID);

			if (isUserExist != null && isToDoIDExist != null)
			{
				var toDo = _mapper.Map<UpdateToDoDTO, ToDoNode>(node);				
				toDo.UpdatedDate = DateTime.UtcNow;
				var toDoEntity = _todorepository.Update(toDo);
				return toDoEntity;
			}
			throw new Exception($"No such ToDo id or User id \n ToDo ID = {node.ID} \n user ID = {node.OwnerId} ");
		}

		public bool DeleteToDo(int id)
		{
			var nodeForDelete = GetByID(id);
			if (nodeForDelete != null) 
			{
				_todorepository.Delete(nodeForDelete);
				return true;
			}
			return false;
			
		}

		public ToDoNode? GetByID(int id)
		{			
			return _todorepository.SingleOrDefault(n => n.Id == id);
		}

		public bool IsDone(int id)
		{
			var node = GetByID(id);			
			if (node != null)
			{
				node.IsDone = true;
				return true;
			}
			throw new Exception("Not fount ID");
		}

		public ToDoNode? UpdateLabel(UpdateToDoLabelDTO label)
		{
			var validator = new UpdateLabeToDoValidator();
			var validatorResult = validator.Validate(label);

			if (!validatorResult.IsValid)
			{
				throw new Exception($"Not valid Label");
			}

			var node = GetByID(label.ID);
			if (node != null) 
			{
				var toDo = _mapper.Map<UpdateToDoLabelDTO, ToDoNode>(label);
	
				return _todorepository.Update(toDo);
			}		
			throw new Exception($"Not fouont ID = {label.ID}");
		}

		public int Count (string? nameFreeText) 
		{ 
			return _todorepository.Count(nameFreeText == null ? null:n=>n.Label.Contains(nameFreeText));
		}

	}
}


