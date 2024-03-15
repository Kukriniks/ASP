
namespace ToDo.BL
{
	using AutoMapper;
	using Common.BL.Exceptions;
	using Common.Domain;
	using Common.Repositories;
	using FluentValidation;
	using Serilog;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using ToDo.BL.Validators;
	using User.Services;

	public class ToDoServices : IToDoServices
	{
		private static IBaseRepository<ToDo>? _todorepository;
		private readonly IUserServices _userRepository;

		private readonly IValidator<CreateToDoDTO> _validator;
		private readonly IMapper _mapper;

		public ToDoServices(IMapper mapper, IUserServices userRepository, IValidator<CreateToDoDTO> validator, IBaseRepository<ToDo> baseRepository)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_validator = validator;
			_todorepository = baseRepository;
		}

		public async Task<IReadOnlyCollection<ToDo>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellationToken = default)
		{
			return await _todorepository.GetAllAsync(
			offset,
			limit,
			nameFreeText == null ? null : n => n.Label.Contains(nameFreeText),
					u => u.Id,
			cancellationToken: cancellationToken);
		}

		public async Task<ToDo> GetByIDAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _todorepository.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
		}

		public async Task<ToDo> AddToDoAsync(CreateToDoDTO node, CancellationToken cancellationToken = default)
		{
			Log.Error("AddToDoAsync Error");
			Log.Information("enter in AddToDoAsync");
			var validatorResult = _validator.Validate(node);
			if (!validatorResult.IsValid)
			{
				throw new BadRequestException("BadRequst");
			}
			var isUserExist = await _userRepository.GetByIDAsync(node.OwnerId);
			if (isUserExist != null)
			{
				var toDo = _mapper.Map<CreateToDoDTO, ToDo>(node);

				toDo.UpdatedDate = DateTime.UtcNow;
				toDo.CreatedDate = DateTime.UtcNow;

				return await _todorepository.AddAsync(toDo, cancellationToken);
			}
			throw new BadRequestException("No Such User");
		}

		public async Task<ToDo> UpdateToDoAsync(UpdateToDoDTO node, CancellationToken cancellation = default)
		{
			Log.Error("UpdateToDoAsync Error");
			Log.Information("enter in UpdateToDoAsync");
			var isUserExist = await _userRepository.GetByIDAsync(node.OwnerId);
			var isToDoIDExist = await _todorepository.SingleOrDefaultAsync(n => n.Id == node.ID);

			if (isUserExist != null && isToDoIDExist != null)
			{
				var toDo = _mapper.Map<UpdateToDoDTO, ToDo>(node);
				var forUpdate = _mapper.Map(node, isToDoIDExist);
				forUpdate.UpdatedDate = DateTime.UtcNow;
				var toDoEntity = await _todorepository.UpdateAsync(forUpdate);
				return toDoEntity;
			}
			throw new NotFoundException($"No such ToDo id or User id \n ToDo ID = {node.ID} \n user ID = {node.OwnerId} ");
		}

		public async Task<ToDo?> UpdateLabelAsync(UpdateToDoLabelDTO label, CancellationToken cancellation = default)
		{
			var validator = new UpdateLabelToDoValidator();
			var validatorResult = validator.Validate(label);

			if (!validatorResult.IsValid)
			{
				throw new BadRequestException("Wrong Label");
			}

			var node = await GetByIDAsync(label.ID);
			if (node != null)
			{
				var toDo = _mapper.Map<UpdateToDoLabelDTO, ToDo>(label);

				return await _todorepository.UpdateAsync(toDo);
			}
			throw new NotFoundException($"Not found ID = {label.ID}");
		}

		public async Task<bool> DeleteToDoAsync(int id, CancellationToken cancellation = default)
		{
			Log.Error("DeleteToDoAsync Error");
			Log.Information("enter in DeleteToDoAsync");

			var nodeForDelete = await GetByIDAsync(id);
			if (nodeForDelete != null)
			{
				await _todorepository.DeleteAsync(nodeForDelete);
				Log.Information("delete" + nodeForDelete);
				return true;
			}

			Log.Error($"Delete Error user id {id}");

			throw new NotFoundException($"user {id}");
		}

		public async Task<bool> IsDoneAsync(int id, CancellationToken cancellation = default)
		{
			var node = await GetByIDAsync(id);
			if (node != null)
			{
				node.IsDone = true;
				return true;
			}
			throw new NotFoundException($"{id} not found");
		}

		public async Task<int> CountAsync(string? nameFreeText, CancellationToken cancellation = default)
		{
			return await _todorepository.CountAsync(nameFreeText == null ? null : n => n.Label.Contains(nameFreeText), cancellation);
		}
	}
}
