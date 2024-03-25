
using Common.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using User.BL.Mapping;
using User.Services;

namespace Users.BL
{
	public static class UsersDependantInjection
	{
		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(UserAutoMapperProfile));
			services.AddTransient<IBaseRepository<Common.Domain.User>, SQLBaseRepository<Common.Domain.User>>();
			services.AddTransient<IUserServices, UserServices>();
			services.AddTransient<IAuthService, AuthService>();

			services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);
			return services;
		}
	}
}
