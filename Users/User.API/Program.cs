
using Common.API;
using Serilog;
using Users.BL;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

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
				builder.Services.AddFluentValidationAutoValidation();
				builder.Services.AddUserServices();
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen(option =>
				{
					option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
					{
						Description = """
										Jwt Authorization header \r\n\r\n
										""",
						Name = "Authorization",
						In = Microsoft.OpenApi.Models.ParameterLocation.Header,
						Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
						Scheme = "Bearer"
					});
				});
				builder.Services.AddAuthorization();
				builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(option =>
					{
						option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = builder.Configuration["Jwt: Issuer"],
							ValidAudience = builder.Configuration["Jwt: Audience"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt: Key"]))
						};
					});
				builder.Services.AddToDoDatabase(builder.Configuration);
				builder.Services.AddControllers()
					.AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = 
					System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

				builder.Host.UseSerilog();



				var app = builder.Build();
				app.UseMiddleware<ExceptionHandlerMiddleWare>();
				// Configure the HTTP request pipeline.
				if (app.Environment.IsDevelopment())
				{
					app.UseSwagger();
					app.UseSwaggerUI();
				}
				
				app.UseHttpsRedirection();

				app.UseAuthentication();

				app.UseAuthorization();
			

				app.MapControllers();

				app.Run();
			}
			catch (Exception e)
			{
				Log.Fatal("Run UserAPI Error" + e.Message);
				throw;
			}
		}
	}
}
