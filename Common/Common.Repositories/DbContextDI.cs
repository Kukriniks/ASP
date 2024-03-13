
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Common.Repositories
{
	public static class ToDoDependencyInjection
	{

		public static IServiceCollection AddToDoDatabase(this IServiceCollection services, IConfiguration configuration) 
		{
			services.AddDbContext<DbContext, ApplicationDBContext>(
				 options =>
				 {
					 options.UseNpgsql(configuration.GetConnectionString("MyConnectionString"));
				 }
				);

			return services;
		}
	}	
}
