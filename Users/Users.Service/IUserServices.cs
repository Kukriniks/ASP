
using Common.Domain;

namespace User.Services
{
	public interface IUserServices
	{
		IReadOnlyCollection<UserNode> GetList(int? offset, string? nameFreeText, int? limit = 10);
		UserNode? GetUserByID(int id);
		UserNode AddUser(UserNode node);
		UserNode UpdateUser(UserNode node);
		bool DeleteUser(int id);
	}
}