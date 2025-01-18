using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Core.Validations
{
    public class CourseValidator : AbstractValidator<CourseDto>
    {
        public CourseValidator() 
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("Title is required !!");
        }
    }
}
