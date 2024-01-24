using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizz.Data;
using Quizz.Entities;

namespace Quizz
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<AppDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
				opt.Password.RequiredLength = 4;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
			}).AddEntityFrameworkStores<AppDbContext>();
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
		}
	}
}