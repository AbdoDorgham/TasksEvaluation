using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.InfraStructure.Data;
using TasksEvaluation.Core.Entities.Business;

namespace TaskEvaluation.InfraStructure.Helpers
{
	public static class DbInitializer
	{
		public static async void Seed(IApplicationBuilder app)
		{
			// Register the context file (ApplicationDbCotext)
			ApplicationDbContext context = app.ApplicationServices.CreateScope()
											.ServiceProvider
											.GetRequiredService<ApplicationDbContext>();

            var userManager = app.ApplicationServices.CreateScope()
                                            .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = app.ApplicationServices.CreateScope()
                                            .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();




            #region Data Will be Seeding in DB
            var courses = new List<Course>
			{
				new Course() { Id = 1 ,Title = "Front End"  },
				new Course() { Id = 2 ,Title = "Back End"  }

			};
			var groups = new List<Group>
			{
				new Group() { Id = 1 , Title = "A" , CourseId = 1 ,} ,
				new Group() { Id = 2 , Title = "B" , CourseId = 1}

			};
			var evaluationGrades = new List<EvaluationGrade>
			{
				new EvaluationGrade() { Id = 1 , Grade = "Excellent"} ,
				new EvaluationGrade() { Id = 2 , Grade = "Very Good"} ,
				new EvaluationGrade() { Id = 3 , Grade = "Good"}

			};

			#endregion


			// Ensure The Courses Table is empty (has no raws)
			if (!context.Courses.Any())
			{
				context.Courses.AddRange(courses);
				context.Database.OpenConnection();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.Courses ON;");
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.Courses OFF;");
				context.Database.CloseConnection();

			}

			// Ensure The Groups Table is empty (has no raws)
			if (!context.Groups.Any())
			{
				context.Groups.AddRange(groups);
				context.Database.OpenConnection();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.Groups ON;");
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.Groups OFF;");
				context.Database.CloseConnection();
			}

			// Ensure The EvaluationGrades Table is empty (has no raws)
			if (!context.EvaluationGrades.Any())
			{
				context.EvaluationGrades.AddRange(evaluationGrades);
				context.Database.OpenConnection();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.EvaluationGrades ON;");
				context.SaveChanges();
				context.Database.ExecuteSqlRaw("Set IDENTITY_INSERT dbo.EvaluationGrades OFF;");
				context.Database.CloseConnection();
			}


			// Seeding main Admin with Role 
            string mainAdminpassword = "2580zamalek";
            IdentityUser mainAdmin = new IdentityUser()
            {
                UserName = "Abdo_Dorgham",
                Email = "abdodorgham257@gmail.com",
                PhoneNumber = "01068117863"

            };

			IdentityRole adminRole = new IdentityRole()
			{
				Name = "admin"
			};
			if(!userManager.Users.Any())
			{
				await userManager.CreateAsync(mainAdmin , mainAdminpassword);
				
			}

            if (!roleManager.Roles.Any())
            {
				await roleManager.CreateAsync(adminRole);
				await userManager.AddToRoleAsync(mainAdmin, adminRole.Name);
            }


        }
	}
}
