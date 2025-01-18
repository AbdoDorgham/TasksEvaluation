using System.ComponentModel.DataAnnotations;

namespace TasksEvaluation.Web.ViewModels.AccountViewModels
{
	public class RegisterViewModel
	{
		[Required, MaxLength(50)]
        public string Username { get; set; }


		[Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }

		[Required,MaxLength(50)]
		public string PhoneNumber { get; set; }

		[Required,DataType(DataType.Password)]
		public string Password { get; set; }

		[Required, DataType(DataType.Password), Compare(nameof(Password) ,ErrorMessage = "Passwords Must be Identical")]
		[Display(Name ="Confirm Password")]
		public string ConfirmPassword { get; set;}



    }
}
