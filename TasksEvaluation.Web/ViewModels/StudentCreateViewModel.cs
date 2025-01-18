using System.ComponentModel.DataAnnotations;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Web.ViewModels
{
	public class StudentCreateViewModel
	{
		public int Id { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public string MobileNumber { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public IFormFile ProfilePic { get; set; }
		public int? GroupId { get; set; }
		public Group Group { get; set; }
	}
}
