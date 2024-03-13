using Common.Domain;
using Common.Repositories;

namespace User.Services
{
	public class UserServices : IUserServices
	{
		private  readonly IBaseRepository<UserNode> _userRepository ;
		
        public UserServices(IBaseRepository<UserNode> userRepository)
        {
			_userRepository = userRepository;

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
			return _userRepository.GetList(
				offset,
				limit,
				nameFreeText == null ? null : n => n.Name.Contains(nameFreeText),
				n => n.Id);
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


