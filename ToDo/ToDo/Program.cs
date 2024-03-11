
using Common.Domain;
using Common.Repositories;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ToDo.BL;
using ToDo.BL.Mapping;
using ToDo.Models;
using User.Services;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
	.WriteTo.File("/Logs/Information-.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
	.WriteTo.File("/Logs/Warning-.txt", Serilog.Events.LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
	.WriteTo.File("/Logs/Error-.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IToDoServices, ToDoServices>();
builder.Services.AddTransient<IBaseRepository<ToDoNode>, BaseRepository<ToDoNode>>();
builder.Services.AddTransient<IBaseRepository<UserNode>, BaseRepository<UserNode>>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddFluentValidationAutoValidation();
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
