using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo
{
	public class ToDoServices
	{
		public static List<ToDo> ToDos= new List<ToDoNode>()
		{
			new ToDo() {Id = 1, Label = "Aaa", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 2, Label = "Aaab", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 3, Label = "Bbba", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 4, Label = "Bbaa", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 5, Label = "Cca", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 6, Label = "Ccaa", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 7, Label = "Eeea", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 8, Label = "Eecc", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 9, Label = "Ddaa", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 10, Label = "ddbb", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 11, Label = "ddcc", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 12, Label = "ccqq", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 13, Label = "ccqq", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 14, Label = "ccrr", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 15, Label = "ffrr", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 16, Label = "rrff", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow},
			new ToDo() {Id = 17, Label = "fffff", IsDone = false, CreatedDate = DateTime.UtcNow, UpdatedDate= DateTime.UtcNow}
		};

		public IReadOnlyCollection<ToDoNode> GetList(string? TextPattern, int? offset, int? limit)
		{
			IEnumerable<ToDoNode> todo = ToDoList.ToDoLists;
			if (!string.IsNullOrWhiteSpace(TextPattern))
			{
				todo = todo.Where(n => n.Label
				.Contains(TextPattern, StringComparison.InvariantCultureIgnoreCase));
			}
			limit ??= 10;

			if (offset.HasValue)
			{
				todo = todo.Skip(offset.Value);
			}

			todo = todo.Take(limit.Value).ToList();
			return todo == null ? NotFound() : Ok(todo);
		}
	}
}
