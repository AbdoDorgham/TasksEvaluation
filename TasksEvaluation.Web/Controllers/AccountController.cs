using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasksEvaluation.Web.ViewModels.AccountViewModels;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace TasksEvaluation.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}

		
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
                
				var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,lockoutOnFailure:false);

				if(result.Succeeded)
					return RedirectToAction(nameof(StudentController.GetAllStudents) ,"Student");
				ModelState.AddModelError(string.Empty, "Invalid Username or Password!!");
			} 
			return View(model);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser()
				{

					UserName = model.Username,
					Email = model.Email,
					PhoneNumber = model.PhoneNumber,
				};
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction(nameof(Login));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User); // .Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value
                if (user == null)
                    return NotFound("The User is not found.");
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is null )
                    return NotFound("The User is not found.");
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(
                    model.Email,
                    "Reset Password",
                    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        public IActionResult ResetPassword(string userId, string code)
        {
            if(userId is null || code is null)
                return NotFound();
            var resetPasswordVM = new ResetPasswordViewModel
            {
                Code = code,
                UserId = userId
            };
            return View(resetPasswordVM);
            
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByIdAsync(resetPasswordViewModel.UserId);

                var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            

            return View(resetPasswordViewModel);


        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }









    }
}


