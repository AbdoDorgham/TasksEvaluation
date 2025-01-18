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
    public class EvaluationGradeValidator : AbstractValidator<EvaluationGradeDto>
    {
        public EvaluationGradeValidator() 
        {
            RuleFor(eg => eg.Grade)
                .NotEmpty()
                .WithMessage("Grade is required !!!");
                
        }
    }
}
