using Common.Domain;
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
			services.AddAutoMapper(typeof(AutoMapperProfile));
			services.AddTransient<IToDoServices, ToDoServices>();
			services.AddTransient<IBaseRepository<UserNode>, SQLBaseRepository<UserNode>>();
			services.AddTransient<IBaseRepository<ToDoNode>, BaseRepository<ToDoNode>>();
			services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);
			return services;
		}
	}	
}
