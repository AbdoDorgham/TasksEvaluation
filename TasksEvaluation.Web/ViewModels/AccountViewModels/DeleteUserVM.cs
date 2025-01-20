using System.ComponentModel.DataAnnotations;

namespace TasksEvaluation.Web.ViewModels.AccountViewModels
{
    public class DeleteUserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
