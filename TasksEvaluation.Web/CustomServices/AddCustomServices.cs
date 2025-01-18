using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskEvaluation.InfraStructure;
using TaskEvaluation.InfraStructure.Repositories.General;
using TaskEvaluation.InfraStructure.Repositories.Services;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Interfaces.IRepositories;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Core.Validations;

namespace TasksEvaluation.Web.CustomServices
{
    public static class AddCustomServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>) , typeof(BaseRepository<>));
            return services;

        }
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AssignmentDto>, AssignmentValidator>();
            services.AddScoped<IValidator<CourseDto>, CourseValidator>();
            services.AddScoped<IValidator<EvaluationGradeDto>, EvaluationGradeValidator>();
            services.AddScoped<IValidator<GroupDto>, GroupValidator>();
            services.AddScoped<IValidator<SolutionDto>, SolutionValidator>();
            services.AddScoped<IValidator<StudentDto>, StudentValidator>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseServices<,>), typeof(BaseServices<,>));
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IEvaluationGradeService, EvaluationGradeService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }

    }
}
