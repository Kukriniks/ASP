namespace ToDo.Models
{
	public class ToDoNode : IToDoNode
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public string Label { get; set; } = default!;
		public bool IsDone { get; set; }
		public DateTime CreatedDate { get; init; }
		public DateTime UpdatedDate { get; set; }

		public ToDoNode(int id, string label, bool _IsDone, DateTime createdDate, DateTime updatedDate, int ownerId)
		{
			Id = id;
			Label = label;
			IsDone = _IsDone;
			CreatedDate = createdDate.ToUniversalTime();
			UpdatedDate = updatedDate.ToUniversalTime();
			OwnerId = ownerId;
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
