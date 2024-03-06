
namespace Common.Domain
{
	public class UserList 
	{
		public List<UserNode> UserNodeList { get; set; }

		public UserList()
		{
			UserNodeList =
			[
				new UserNode(id: 1, name: "Name 1"),
				new UserNode(id: 2, name: "Name 2"),
				new UserNode(id: 3, name: "Name 3"),
				new UserNode(id: 4, name: "Name 4"),
				new UserNode(id: 5, name: "Name 5")
			];
		}
	}
}
