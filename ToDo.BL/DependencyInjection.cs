using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ToDo.BL.Mapping;
using FluentValidation;
using System.Reflection;

namespace ToDo.BL
{
	public static class ToDoDependencyInjection
	{
		public static IServiceCollection AddToDoServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ToDoAutoMapperProfile));
			services.AddTransient<IToDoServices, ToDoServices>();
			services.AddTransient<IBaseRepository<Common.Domain.User>, SQLBaseRepository<Common.Domain.User>>();
			services.AddTransient<IBaseRepository<Common.Domain.ToDo>, SQLBaseRepository<Common.Domain.ToDo>>();
			services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);
			return services;
		}
	}	
}
