
namespace User.Services
{
	public interface IUserServices
	{
		public Task<IReadOnlyCollection<Common.Domain.User>> GetListAsync(int? offset, string? nameFreeText, int? limit = 10, CancellationToken cancellationToken = default);
		public Task<Common.Domain.User?> GetByIDAsync(int id, CancellationToken cancellationToken = default);
		public Task<Common.Domain.User> AddUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default);
		public Task<Common.Domain.User> UpdateUserAsync(Common.Domain.User node, CancellationToken cancellationToken = default);
		public Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default);
	}
}