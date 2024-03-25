using Common.Repositories;
using Common.BL.Exceptions;
using Users.BL.UserDTO;
using Common.Domain;
using Common.Auth.Application.Utils;
using AutoMapper;

namespace User.Services
{
	public class UserServices : IUserServices
	{
		private  readonly IBaseRepository<Common.Domain.User> _userRepository ;
		private readonly IMapper _mapper;

		public UserServices(IBaseRepository<Common.Domain.User> userRepository, IMapper mapper)
        {
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<GetUserDTO> AddUserAsync(CreateUserDTO dto, CancellationToken cancellationToken = default)
		{
			
            if ((await _userRepository.SingleOrDefaultAsync(u=>u.Login == dto.Login)) is not null)
            {
				throw new BadRequestException("User is exist");
            }
            var entity = new Common.Domain.User()
			{
				Login = dto.Login.Trim(),
				Name = dto.Name,
				PasswordHash = PasswordHasher.HashPassword(dto.Password)

			};
			var user = await _userRepository.AddAsync(entity, cancellationToken);
			var result = new GetUserDTO();
			var userDto = _mapper.Map(user, result);
			return result;
		}

		public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
		{
			var userForDelete = await _userRepository.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
			if (userForDelete != null)
			{
				return await _userRepository.DeleteAsync(userForDelete, cancellationToken);
			}

			throw new NotFoundException(new { id = id });
		}

		public async Task<GetUserDTO?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
		{
			return _mapper.Map<GetUserDTO>(await _userRepository.SingleOrDefaultAsync(u => u.Id == id, cancellationToken));
		}

		public async Task<IReadOnlyCollection<GetUserDTO>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellationToken = default)
		{
			var user = await _userRepository.GetAllAsync(
				offset,
				limit,
				nameFreeText == null ? null : n => n.Name.Contains(nameFreeText),
				u => u.Id,
				cancellationToken: cancellationToken);

			return (IReadOnlyCollection<GetUserDTO>)_mapper.Map<GetUserDTO>(user);

		}

		public async Task<GetUserDTO> UpdateUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default)
		{
			return _mapper.Map<GetUserDTO>(await _userRepository.UpdateAsync(node, cancellationToken));
		}
	}
}


