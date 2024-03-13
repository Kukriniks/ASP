namespace Common.Domain
{
	public class UserNode 
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public ICollection<ToDoNode> Todos { get; set; } = default!;

		public UserNode(int id, string name)
		{
			Id = id;
			Name = name;
		}
		public UserNode()
		{
		}
	}
}
