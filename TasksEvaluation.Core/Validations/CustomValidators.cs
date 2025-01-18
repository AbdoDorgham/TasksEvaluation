using FluentValidation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksEvaluation.Core.Validations
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> MatchPhoneNumberRule<T>(this IRuleBuilder<T, string> ruleBuilder) =>
             ruleBuilder.SetValidator(new RegularExpressionValidator<T>(@"01[0125][0-9]{8}$"));

	
		public static IRuleBuilderOptions<T, string> EndsWithCustomExtensions<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            List<string> extensions = new List<string>() { ".png", ".jpg", ".jpeg", ".zip", ".pdf" };
            return ruleBuilder.Must(s => extensions.Any(s.EndsWith));
            
        }


        //      public static bool EndsWithCustomExtensions(this string str)
        //      {
        //	List<string> extensions = new List<string>() { ".png", ".jpg", ".jpeg", ".zip", ".pdf" };
        //          return extensions.Any(str.EndsWith);
        //          //if (extensions.Any(str.EndsWith))
        //          //    return true;
        //          //foreach (string ext in extensions)
        //          //{
        //          //    if (str.EndsWith(ext))
        //          //        return true;
        //          //}
        //          //return false;
        //}





    }
}
