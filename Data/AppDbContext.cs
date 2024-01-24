using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizz.Entities;

namespace Quizz.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext()
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<AppUser> Users{ get; set; }
		public DbSet<Quiz> Quizzes { get; set; }
		public DbSet<Question> Questiones { get; set; }
		public DbSet<Option> Options { get; set; }
	}
}
