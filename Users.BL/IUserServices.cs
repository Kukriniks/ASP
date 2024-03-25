
using Users.BL.UserDTO;

namespace User.Services
{
	public interface IUserServices
	{
		public Task<IReadOnlyCollection<GetUserDTO>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellationToken = default);
		public Task<GetUserDTO?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
		public Task<GetUserDTO> AddUserAsync(CreateUserDTO dto, CancellationToken cancellationToken = default);
		public Task<GetUserDTO> UpdateUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default);
		public Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default);

	}
}