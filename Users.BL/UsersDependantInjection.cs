
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using User.Services;

namespace Users.BL
{
	public static class UsersDependantInjection
	{
		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			services.AddTransient<IBaseRepository<Common.Domain.User>, SQLBaseRepository<Common.Domain.User>>();
			services.AddTransient<IUserServices, UserServices>();
			return services;
		}
	}
}
