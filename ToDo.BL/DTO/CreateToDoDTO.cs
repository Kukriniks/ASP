namespace ToDo.BL
{
	public class CreateToDoDTO
	{
		public int OwnerId { get; set; }
		public string Label { get; set; } = default!;
		public bool IsDone { get; set; }
    }
}
