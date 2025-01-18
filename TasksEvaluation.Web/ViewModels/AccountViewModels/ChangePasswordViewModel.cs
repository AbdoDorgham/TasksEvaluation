using System.ComponentModel.DataAnnotations;

namespace TasksEvaluation.Web.ViewModels.AccountViewModels
{
    public class ChangePasswordViewModel
    {
        [Required, DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(NewPassword), ErrorMessage = "Passwords Must be Identical")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
