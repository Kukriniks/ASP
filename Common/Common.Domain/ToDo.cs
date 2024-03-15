using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Domain
{
	public class ToDo 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int OwnerId { get; set; }
		
		public string Label { get; set; } = default!;

		public bool IsDone { get; set; }

		public User Owner { get; set; } = default!;

		public DateTime CreatedDate { get; set; }

		public DateTime UpdatedDate { get; set; }

	}
}
