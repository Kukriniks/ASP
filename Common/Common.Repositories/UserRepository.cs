using Common.Domain;
namespace ToDo.ToDo.Repositories
{
	public class UserRepository : IUserRepository
	{
		public static UserList userList = new();

		public IUserNode? GetUserByID(int id)
		{
			return userList.UserNodeList.FirstOrDefault(x => x.Id == id);
		}

		public IUserNode AddUser(IUserNode user)
		{
			int id = 1;
			if (userList.UserNodeList.Count > 0)
			{
				id = userList.UserNodeList.Select(l => l.Id).Max() + 1;
			}
			user.Id = id;
			userList.UserNodeList.Add(user);
			return user;
		}

		public IUserNode UpdateUser(IUserNode user)
		{
			var userEntity = userList.UserNodeList.FirstOrDefault(t => t.Id == user.Id);
			if (userEntity != null)
			{
				user.Name = userEntity.Name;
				return userEntity;
			}
			return null;
		}

		public bool DeleteUser(int id)
		{
			var userEntity = userList.UserNodeList.FirstOrDefault(t => t.Id == id);
			if (userEntity != null)
			{
				userList.UserNodeList.Remove(userEntity);
				return true;
			}
			return false;
		}

		public IEnumerable<IUserNode> GetList()
		{
			return userList.UserNodeList;
		}
	}
}
