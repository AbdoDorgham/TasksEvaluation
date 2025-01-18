using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Core.Validations;
using TasksEvaluation.Web.ViewModels;

namespace TasksEvaluation.Web.Controllers
{
    [Authorize]
	public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IStudentService studentService , IUnitOfWork unitOfWork ,IGroupService groupService)
        {
            _studentService = studentService;
            _unitOfWork = unitOfWork;
            _groupService = groupService;
                
        }
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetStudentsIncludeGroups();
            return View(students);
        }


        public ActionResult Create()
        {
            var selectedGroup = new GroupDto() { Id = 0, Title = "Select Group" };
            var allGroups = _groupService.GetAll().Result.ToList();
            allGroups.Add(selectedGroup);

            ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
                , allGroups.Find(c => c.Id == 0).Id);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDto studentDto)   
        {
              
			if (ModelState.IsValid)
            {

				if (studentDto.ProfilePicFile != null)
				{
					string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
					string fileName = Guid.NewGuid() + Path.GetExtension(studentDto.ProfilePicFile.FileName);
					string filePath = Path.Combine(uploadsFolder, fileName);


					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await studentDto.ProfilePicFile.CopyToAsync(fileStream);
					}

					studentDto.ProfilePic = @$"/uploads/{fileName}" ;
				}

				await _studentService.Create(studentDto);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(GetAllStudents));
            }
			var selectedGroup = new GroupDto() { Id = 0, Title = "Select Group" };
			var allGroups = _groupService.GetAll().Result.ToList();
			allGroups.Add(selectedGroup);
			ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
				, studentDto.GroupId);
			return View(studentDto);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null || !_studentService.Any().Result)
                return NotFound();
            var studentDto = await _studentService.GetById(id.Value);

			var allGroups = _groupService.GetAll().Result.ToList();
			ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
				, studentDto.GroupId);
			return View(studentDto);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id , StudentDto studentDto)// , IFormFile ProfilePic
		{
            if (studentDto.Id != id)
                return NotFound();
            studentDto.ProfilePic = _studentService.GetById(id).Result.ProfilePic;
			


			if (ModelState.IsValid)
            {

				if (!(studentDto.ProfilePicFile is null))
				{
					string picFileName = studentDto.ProfilePicFile.FileName;
					string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
					string picPath = Path.Combine(uploadsFolderPath, studentDto.ProfilePicFile.FileName);
					if (!System.IO.File.Exists(picPath))
					{
						picFileName = Guid.NewGuid() + Path.GetExtension(studentDto.ProfilePicFile.FileName);
						string newPicPath = Path.Combine(uploadsFolderPath, picFileName);
						using (var fileStream = new FileStream(newPicPath, FileMode.Create))
						{
							await studentDto.ProfilePicFile.CopyToAsync(fileStream);
						}
					}
					studentDto.ProfilePic = @$"/uploads/{picFileName}";
				}
				try
				{
                    _studentService.Update(studentDto);

                    await _unitOfWork.Complete();
                }
                catch (DBConcurrencyException)
                {
                    if (!_studentService.Contains(studentDto).Result)
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(GetAllStudents));
            }
			var allGroups = _groupService.GetAll().Result.ToList();
			ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
				, studentDto.GroupId);
			return View(studentDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || !_studentService.Any().Result)
                return NotFound();
            var student = await _studentService.GetById(id.Value);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ( !_studentService.Any().Result)
                return NotFound();

            _studentService.Delete(id);
			await _unitOfWork.Complete();
			return RedirectToAction(nameof(GetAllStudents));
        }

        [AllowAnonymous]
        public IActionResult LoginAsStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult LoginAsStudent( LoginAsStudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                if (! _studentService.Contains(s => s.Email == studentVM.Email).Result)
                      return NotFound("Student Email Not Found!!!");
                var student = _studentService.GetStudentsIncludeGroups().Result.Where(std => std.Email == studentVM.Email).FirstOrDefault();
				// ViewData["Student"] = student;
				return RedirectToAction(nameof(AssignmentController.GetAssignmentsAccordingSpecificStudent), "Assignment", student);

			}
			return View(studentVM);
        }


    }
}
