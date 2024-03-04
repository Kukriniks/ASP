using Common.Domain;

	public interface IUserRepository
    {
		IEnumerable<IUserNode> GetList();
		IUserNode? GetUserByID(int id);
		IUserNode AddUser(IUserNode node);
		IUserNode UpdateUser(IUserNode node);
        bool DeleteUser (int id);	
	}
