
using EcommerceApi.Data;
using EcommerceApi.Interfaces;
using EcommerceApi.Models;
using EcommerceApi.Repositories;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

			// -------------------------------------------------------------------------------------------

			builder.Services.AddSwaggerGen(option =>
			{
				option.SwaggerDoc("v1", new OpenApiInfo { Title = "StockMarketAPI", Version = "v1" });
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			});

			// -------------------------------------------------------------------------------------------

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConn1"));
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
			}).AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme =
				options.DefaultChallengeScheme =
				options.DefaultForbidScheme =
				options.DefaultScheme =
				options.DefaultSignInScheme =
				options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
				{
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = builder.Configuration["JWT:Issuer"],
						ValidAudience = builder.Configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true
					};
				});

			// Dependency Injections
			builder.Services.AddScoped<IProductRepo, ProductRepo>();
			builder.Services.AddScoped<ICommentRepo, CommentRepo>();
			builder.Services.AddScoped<ITokenService, TokenService>();

			// Authorization
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireOwnerRole", policy => policy.RequireRole("Owner"));
			});

			var app = builder.Build();

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
	}
}
