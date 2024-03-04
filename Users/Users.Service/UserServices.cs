using Common.Domain;

namespace User.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
			_userRepository = userRepository;
        }

		public IUserNode AddUser(IUserNode user)
		{
			_userRepository.AddUser(user);
			return user;
		}

		public bool DeleteUser(int id)
		{
			return _userRepository.DeleteUser(id);
		}

		public IEnumerable<IUserNode> GetList()
		{
			return _userRepository.GetList();
		}

		public IUserNode? GetUserByID(int id)
		{
			return _userRepository.GetUserByID(id);
		}

		public IUserNode UpdateUser(IUserNode user)
		{
			return _userRepository.UpdateUser(user);
		}
	}
}


