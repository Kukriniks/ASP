using ToDo.Models;


namespace ToDo.Repositories
{
	public class ToDoRepository //:IToDoRepository
	{
		public static ToDoList toDoList = new();

		public IEnumerable<IToDoNode> GetList(string? TextPattern, int? offset, int? limit)
		{
			IEnumerable<IToDoNode> todo = toDoList.ToDoNodeList;

			if (!string.IsNullOrWhiteSpace(TextPattern))
			{
				todo = todo.Where(n => n.Label.Contains(TextPattern, StringComparison.InvariantCultureIgnoreCase));
			}
			limit ??= 10;

			if (offset.HasValue)
			{
				todo = todo.Skip(offset.Value);
			}

			todo = todo.Take(limit.Value).ToList();
			return todo;
		}


		public IToDoNode? GetByID(int id)
		{
			return toDoList.ToDoNodeList.SingleOrDefault(n => n.Id == id);
		}

		public ToDoNode AddToDo(ToDoNode node)
		{
			var time = DateTime.Now;
			int id = 1;
			if (toDoList.ToDoNodeList.Count > 0)
			{
				id = toDoList.ToDoNodeList.Select(l => l.Id).Max() + 1;
			}

			node.CreatedDate = time;
			node.UpdatedDate = time;
			node.Id = id;			
			toDoList.ToDoNodeList.Add(node);
			return node;
		}

		public ToDoNode? UpdateToDo(ToDoNode node)
		{
			var toDoEntity = toDoList.ToDoNodeList.SingleOrDefault(t => t.Id == node.Id);

			if (toDoEntity != null)
			{
				toDoEntity.UpdatedDate = DateTime.Now;
				toDoEntity.OwnerId = node.OwnerId;
				toDoEntity.Label = node.Label;
				toDoEntity.IsDone = node.IsDone;
				return (ToDoNode)toDoEntity;
			}
			return null;
		}

		public bool DeleteToDo(int id)
		{
			var todo = toDoList.ToDoNodeList.SingleOrDefault(t => t.Id == id);
			if (todo != null)
			{
				toDoList.ToDoNodeList.Remove(todo);
				return true;
			}
			return false;
		}

		public IToDoNode? SetAsDone(int id)
		{
			var node = toDoList.ToDoNodeList.FirstOrDefault(n => n.Id == id);

			if (node != null)
			{
				node.IsDone = true;
				return node;
			}
			else { return null; }
		}

		public IToDoNode? UpdateLabel(string label, int id)
		{
			var node = GetByID(id);
			if (node != null)
			{
				node.Label = label;
				node.UpdatedDate = DateTime.UtcNow;
			}
			return node;
		}
	}
}

