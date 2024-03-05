﻿namespace Common.Domain
{
	public class UserNode : IUserNode
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;

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
