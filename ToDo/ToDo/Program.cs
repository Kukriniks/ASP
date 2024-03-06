
using Common.Domain;
using Common.Repositories;
using ToDo.BL;
using ToDo.Models;
using ToDo.Repositories;
using User.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IToDoServices, ToDoServices>();
//builder.Services.AddTransient<IToDoRepository, ToDoRepository>();
builder.Services.AddTransient<IRepository<ToDoNode>, BaseRepository<ToDoNode>>();
builder.Services.AddTransient<IUserServices, UserServices>();

builder.Services.AddAutoMapperToDoBL();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
