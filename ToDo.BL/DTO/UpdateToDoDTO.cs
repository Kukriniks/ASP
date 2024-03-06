using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.BL
{
	public class UpdateToDoDTO
	{
		public int ID { get; set; }
		public int OwnerId { get; set; }
		public string Label { get; set; } = default!;
		public bool IsDone { get; set; }
	}
}
