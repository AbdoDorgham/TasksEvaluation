using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasksEvaluation.Web.ViewModels.RoleViewModels;

namespace TasksEvaluation.Web.Controllers
{

	[Authorize(Roles ="admin")]
	public class RoleController : Controller
	{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

                
        }
        public IActionResult AssignRoleToUser()
		{
			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserVM assignRoleToUserVM)
        {

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(assignRoleToUserVM.UserName);
                var isExistRole = await _roleManager.RoleExistsAsync(assignRoleToUserVM.RoleName);
                if (user is null || !isExistRole)
                    return NotFound();
                var assignResult = await _userManager.AddToRoleAsync(user, assignRoleToUserVM.RoleName);
                if (assignResult.Succeeded)
                    return RedirectToAction("GetAllStudents", "Student");

                
            }
            return View(assignRoleToUserVM);
        }


        public IActionResult RemoveRoleFromUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RemoveRoleFromUser(AssignRoleToUserVM assignRoleToUserVM)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(assignRoleToUserVM.UserName);
                var isExistRole = await _roleManager.RoleExistsAsync(assignRoleToUserVM.RoleName);
                if (user is null || !isExistRole)
                    return NotFound();
                var assignResult = await _userManager.RemoveFromRoleAsync(user, assignRoleToUserVM.RoleName);
                if (assignResult.Succeeded)
                    return RedirectToAction("GetAllStudents", "Student");


            }
            return View(assignRoleToUserVM);
        }
    }
}
