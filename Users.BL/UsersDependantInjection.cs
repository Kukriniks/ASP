using Common.Domain;
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using User.Services;

namespace Users.BL
{
	public static class UsersDependantInjection
	{
		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			services.AddTransient<IBaseRepository<UserNode>, SQLBaseRepository<UserNode>>();
			services.AddTransient<IUserServices, UserServices>();


			return services;
		}
	}
}
