using System.ComponentModel.DataAnnotations;

namespace TasksEvaluation.Web.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
