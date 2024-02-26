namespace ToDo
{
	public class ToDoNode
	{
		public int Id { get; init; }
		public string Label { get; set; } = default!;
		public bool IsDone { get; set; }
		public DateTime CreatedDate { get; init; }
		public DateTime UpdatedDate { get; set; }

		public ToDoNode(int id, string label, bool _IsDone, DateTime createdDate, DateTime updatedDate)
		{
			Id = id;
			Label = label;
			IsDone = _IsDone;
			CreatedDate = createdDate.ToUniversalTime();
			UpdatedDate = updatedDate.ToUniversalTime();
		}
		public ToDoNode()
		{
		}

		public override string ToString()
		{
			return $"id = {Id} \n Label = {Label} \n IsDone = {IsDone} \n CreatedDate = {CreatedDate} \n UpdatedDate = {UpdatedDate}";
		}
	}
}
