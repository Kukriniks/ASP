using Common.Repositories;
using Common.BL.Exceptions;

namespace User.Services
{
	public class UserServices : IUserServices
	{
		private  readonly IBaseRepository<Common.Domain.User> _userRepository ;
		
        public UserServices(IBaseRepository<Common.Domain.User> userRepository)
        {
			_userRepository = userRepository;

		}

		public async Task<Common.Domain.User> AddUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default)
		{
			return await _userRepository.AddAsync(node, cancellationToken);
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

		public async Task<Common.Domain.User?> GetByIDAsync(int id, CancellationToken cancellationToken = default)
		{
			return await _userRepository.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
		}

		public async Task<IReadOnlyCollection<Common.Domain.User>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellationToken = default)
		{
			return await _userRepository.GetAllAsync(
				offset,
				limit,
				nameFreeText == null ? null : n => n.Name.Contains(nameFreeText),
				u => u.Id,
				cancellationToken: cancellationToken);
		}

		public async Task<Common.Domain.User> UpdateUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default)
		{
			return await _userRepository.UpdateAsync(node, cancellationToken);
		}
	}
}


