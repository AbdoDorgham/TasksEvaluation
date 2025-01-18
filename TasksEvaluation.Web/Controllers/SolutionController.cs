using Microsoft.AspNetCore.Mvc;
using TaskEvaluation.InfraStructure.Repositories.Services;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Web.ViewModels;

namespace TasksEvaluation.Web.Controllers
{
	public class SolutionController : Controller
	{

		private readonly ISolutionService _solutionService;
		private readonly IStudentService _studentService;
		private readonly IUnitOfWork _unitOfWork;

		public SolutionController(ISolutionService solutionService, IUnitOfWork unitOfWork , IStudentService studentService)
		{
			_solutionService = solutionService;
			_unitOfWork = unitOfWork;
			_studentService = studentService;


		}

		public async Task<IActionResult> GetAllSolutions()
		{
			var solutions = await  _solutionService.GetAll();
			return View(solutions);
		}

		public IActionResult Create(int studentId , int assignmentId) // , string studentEmail
		{
			ViewData["StudentId"] = studentId;
			ViewData["AssignmentId"] = assignmentId;
			//var loginAsStudentVM = new LoginAsStudentVM();
			//loginAsStudentVM.Email = studentEmail;
			//ViewData["LoginAsStudentVM"] = loginAsStudentVM;
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SolutionCreateVM solutionVM)
		{
			if (ModelState.IsValid)
			{
				var solution = new SolutionDto()
				{
					AssignmentId = solutionVM.AssignmentId,
					StudentId = solutionVM.StudentId,
					Notes = solutionVM.Notes
				};
				if (solutionVM.SolutionFile != null)
				{
					string uploadsSolutionFilesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads" ,"SolutionFiles");
					string fileName = Guid.NewGuid() + Path.GetExtension(solutionVM.SolutionFile.FileName);
					string filePath = Path.Combine(uploadsSolutionFilesFolder, fileName);


					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await solutionVM.SolutionFile.CopyToAsync(fileStream);
					}

					solution.SolutionFile = @$"/uploads/SolutionFiles/{fileName}";
				}
				await _solutionService.Create(solution);
				await _unitOfWork.Complete();
				var student = _studentService.GetById(solution.StudentId.Value).Result;
				return RedirectToAction(nameof(AssignmentController.GetAssignmentsAccordingSpecificStudent) ,"Assignment" , student);

			}
			return View(solutionVM);

		}

	}
}
