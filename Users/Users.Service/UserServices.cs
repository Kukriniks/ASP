using Common.Domain;
using Common.Repositories;

namespace User.Services
{
	public class UserServices : IUserServices
	{
		private readonly IRepository<UserNode> _userRepository;
		public UserServices(IRepository<UserNode> userRepository)
		{
			_userRepository = userRepository;
			_userRepository.Add(new UserNode { Id = 1,Name = "one" });
			_userRepository.Add(new UserNode { Id = 2, Name = "two" });
			_userRepository.Add(new UserNode { Id = 3, Name = "three" });
			_userRepository.Add(new UserNode { Id = 4, Name = "four" });
			_userRepository.Add(new UserNode { Id = 5, Name = "one" });

		}
		

		public UserNode AddUser(UserNode user)
		{
			return _userRepository.Add(user);
			
		}

		public bool DeleteUser(int id)
		{
			var userForeDelet = GetUserByID(id);
			return _userRepository.Delete(userForeDelet);
		}

		public IReadOnlyCollection<UserNode> GetList(int? offset, string? nameFreeText = null, int? limit = 10)
		{
			return _userRepository.GetList(offset, limit, u =>u.Name.Contains(nameFreeText), u => u.Id);
		}

		public UserNode? GetUserByID(int id)
		{
			return _userRepository.SingleOrDefault(u => u.Id==id);
		}

		public UserNode UpdateUser(UserNode user)
		{
			return _userRepository.Update(user);
		}



	}
}


