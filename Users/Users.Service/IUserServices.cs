
using Common.Domain;

namespace User.Services
{
	public interface IUserServices
	{
		IReadOnlyCollection<Common.Domain.User> GetList(int? offset, string? nameFreeText, int? limit = 10);
		Common.Domain.User? GetUserByID(int id);
		Common.Domain.User AddUser(Common.Domain.User node);
		Common.Domain.User UpdateUser(Common.Domain.User node);
		bool DeleteUser(int id);
	}
}