
using System.Text.Json;

namespace Common.BL.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(object filter) : base("Not Found by filter " + JsonSerializer.Serialize(filter))
		{

		}
	}
}
