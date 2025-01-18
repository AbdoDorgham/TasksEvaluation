using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Core.Validations
{
    public class SolutionValidator : AbstractValidator<SolutionDto>
    {
        public SolutionValidator() 
        {
            RuleFor(s => s.SolutionFile)
                .NotEmpty()
                .WithMessage("Solution File is required !!!")
                .EndsWithCustomExtensions()
                .WithMessage($"Solution File Must Ends With Valid Extension !!! ");




        }
    }
}
