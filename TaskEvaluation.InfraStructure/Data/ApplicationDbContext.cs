using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.InfraStructure.EntityConfigs;
using TasksEvaluation.Core.Entities.Business;

namespace TaskEvaluation.InfraStructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<Course> Courses { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<Solution> Solutions { get; set; }
		public DbSet<EvaluationGrade> EvaluationGrades { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Configure the IdentityUserLogin<string> entity to have no key
			modelBuilder.Entity<IdentityUserLogin<string>>()
			.HasKey(l => new { l.LoginProvider, l.ProviderKey });
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
		}


	}
}
