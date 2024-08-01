
using EcommerceApi.Data;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using EcommerceApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConn1"));
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				// Password settings.
				// options.Password.RequireDigit = true;
				// options.Password.RequireLowercase = true;
				// options.Password.RequireNonAlphanumeric = true;
				// options.Password.RequireUppercase = true;
				// options.Password.RequiredLength = 4;
				// options.Password.RequiredUniqueChars = 1;
				// 
				// // Lockout settings.
				// options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				// options.Lockout.MaxFailedAccessAttempts = 5;
				// options.Lockout.AllowedForNewUsers = true;
				// 
				// // User settings.
				// options.User.AllowedUserNameCharacters =
				// "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				// options.User.RequireUniqueEmail = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>();

			// Dependency Injections
			builder.Services.AddScoped<IProductRepo, ProductRepo>();
			builder.Services.AddScoped<ICommentRepo, CommentRepo>();

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
	}
}
