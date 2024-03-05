using Microsoft.Extensions.DependencyInjection;
using ToDo.BL.Mapping;

namespace ToDo.BL
{
	public static class ToDoDependencyInjection
	{
		//public static void AddServices(IServiceCollection collection)
		//{
		//	collection.AddAutoMapper(typeof(AutoMapperProfile));
		//}

		public static IServiceCollection AddAutoMapperToDoBL(this IServiceCollection services)
				=> services.AddAutoMapper(typeof(AutoMapperProfile));
	}
}
