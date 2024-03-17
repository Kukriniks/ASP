
using Common.Domain;
using Common.Repositories;
using Serilog;
using User.Services;
using Users.BL;

namespace User.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
			.WriteTo.File("./Logs/Information-.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
			.WriteTo.File("./Logs/Warning-.txt", Serilog.Events.LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
			.WriteTo.File("./Logs/Error-.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
			.CreateLogger();

			// Add services to the container.

			try
			{
				builder.Services.AddControllers();
				// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
				builder.Services.AddTransient<IUserServices, UserServices>();
				builder.Services.AddUserServices();
				builder.Services.AddToDoDatabase(builder.Configuration);
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();
				builder.Host.UseSerilog();

				var app = builder.Build();

				// Configure the HTTP request pipeline.
				if (app.Environment.IsDevelopment())
				{
					app.UseSwagger();
					app.UseSwaggerUI();
				}

				app.UseHttpsRedirection();

				app.UseAuthorization();

				app.MapControllers();

				app.Run();
			}
			catch (Exception e)
			{
				Log.Error("Run UserAPI Error" + e.Message);
				throw;
			}
		}
	}
}
