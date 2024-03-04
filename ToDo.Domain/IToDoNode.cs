
namespace ToDo.ToDo.Models
{
	public interface IToDoNode
	{		
		int Id { get; set; }
		bool IsDone { get; set; }
		string Label { get; set; }
		int OwnerId { get; set; }
		DateTime CreatedDate { get; init; }
		DateTime UpdatedDate { get; set; }		
		
	}
}