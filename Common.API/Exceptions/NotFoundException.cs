using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.API.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(object filter)  : base ("Not Found by filter " + JsonSerializer.Serialize(filter))
		{ 

		}
	}
}
