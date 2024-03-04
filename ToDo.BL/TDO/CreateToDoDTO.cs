
// Ignore Spelling: TDO

namespace ToDo.BL
{
	internal class CreateToDoDTO
	{
		public int OwnerId { get; set; }
		public string Label { get; set; } = default!;
		public bool IsDone { get; set; }
	}
}
