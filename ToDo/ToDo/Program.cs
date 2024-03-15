
using Common.API;
using Common.Domain;
using Common.Repositories;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ToDo.BL;
using ToDo.BL.Mapping;
using Users.BL;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
	.WriteTo.File("./Logs/Information-.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
	.WriteTo.File("./Logs/Warning-.txt", Serilog.Events.LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
	.WriteTo.File("./Logs/Error-.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
	.CreateLogger();

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	builder.Services.AddControllers();
	builder.Services.AddFluentValidationAutoValidation();
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddToDoServices();
	builder.Services.AddUserServices();
	builder.Services.AddSwaggerGen();
	builder.Host.UseSerilog();
	builder.Services.AddToDoDatabase(builder.Configuration);
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
	app.UseMiddleware<ExceptionHandlerMiddleWare>();

	app.Run();
}
catch (Exception e)
{
	Log.Error(e, "Run Error");
	throw;
}
