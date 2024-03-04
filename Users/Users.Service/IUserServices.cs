
using Common.Domain;

namespace User.Services
{ 
    public interface IUserServices
    {
		IEnumerable<IUserNode> GetList();
		IUserNode? GetUserByID(int id);
		IUserNode AddUser(IUserNode node);
		IUserNode UpdateUser(IUserNode node);
		bool DeleteUser(int id);
	}
}