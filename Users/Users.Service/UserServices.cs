using Common.Domain;
using Common.Repositories;

namespace User.Services
{
	public class UserServices : IUserServices
	{
		private  readonly IBaseRepository<Common.Domain.User> _userRepository ;
		
        public UserServices(IBaseRepository<Common.Domain.User> userRepository)
        {
			_userRepository = userRepository;

		}

        public Common.Domain.User AddUser(Common.Domain.User user)
		{
			return _userRepository.Add(user);			
		}

		public bool DeleteUser(int id)
		{
			var userForeDelet = GetUserByID(id);
			return _userRepository.Delete(userForeDelet);
		}

		public IReadOnlyCollection<Common.Domain.User> GetList(int? offset, string? nameFreeText = null, int? limit = 10)
		{
			return _userRepository.GetList(
				offset,
				limit,
				nameFreeText == null ? null : n => n.Name.Contains(nameFreeText),
				n => n.Id);
		}

		public Common.Domain.User? GetUserByID(int id)
		{
			return _userRepository.SingleOrDefault(u => u.Id==id);
		}

		public Common.Domain.User UpdateUser(Common.Domain.User user)
		{
			return _userRepository.Update(user);
		}

    }
}


