using System.ComponentModel.DataAnnotations;

namespace TasksEvaluation.Web.ViewModels.RoleViewModels
{
	public class AssignRoleToUserVM
	{

		[Required]
        public string UserName { get; set; }

		[Required]
		public string RoleName { get; set; }
	}
}
