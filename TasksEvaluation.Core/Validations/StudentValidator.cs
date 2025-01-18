using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Core.Validations
{

    public class StudentValidator : AbstractValidator<StudentDto>
    {

        public StudentValidator() 
        {
            RuleFor(s => s.FullName)
                .NotEmpty()
                .WithMessage("FullName is required !!!");
            RuleFor(s => s.MobileNumber)
                .NotEmpty()
                .WithMessage("Phone Number is required !!!")
                .MatchPhoneNumberRule()
                .WithMessage("Please provide valid phone number");
            RuleFor(s => s.Email)
                .NotEmpty()
                .WithMessage("Email is required !!!")
                .EmailAddress()
               .WithMessage("Please Enter Valid Email Address !!!");
            RuleFor(s => s.ProfilePicFile)
            .Must(ValidateFileExtension)
            .WithMessage("Profile picture must be a .pdf, .jpg, .jpeg, .png, or .zip file.");
            RuleFor(s => s.GroupId)
                .NotEqual(0)
                .WithMessage("Please Choose Group.");




        }

        public bool ValidateFileExtension(IFormFile file)
        {
            if (file is null || string.IsNullOrEmpty(file.FileName))
                return true;
			var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".zip" };
			var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
			return allowedExtensions.Contains(extension);
			
       
        }
    }
}
