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
    public class AssignmentValidator : AbstractValidator<AssignmentDto>
    {
        public AssignmentValidator() 
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("Title is required !!!");
            RuleFor(a => a.Description)
                .MaximumLength(250);
            RuleFor(a => a.Deadline)
                .GreaterThan(DateTime.Now);

            
        }
    }
}
