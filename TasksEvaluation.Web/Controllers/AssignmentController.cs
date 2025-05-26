using Microsoft.AspNetCore.Mvc;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TaskEvaluation.InfraStructure.Repositories.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Web.ViewModels;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.WebSockets;


namespace TasksEvaluation.Web.Controllers
{
	[Authorize]
	public class AssignmentController : Controller
	{

		private readonly IAssignmentService _assignmentService;
		private readonly IGroupService _groupService;
		private readonly IEvaluationGradeService _evaluationGradeService;
		private readonly IUnitOfWork _unitOfWork;
		public AssignmentController(IAssignmentService assignmentService, IUnitOfWork unitOfWork, IGroupService groupService, IEvaluationGradeService evaluationGradeService)
		{
			_assignmentService = assignmentService;
			_unitOfWork = unitOfWork;
			_groupService = groupService;
			_evaluationGradeService = evaluationGradeService;
		}
	
		public async Task<IActionResult> GetAllAssignments()
		{
			var assignments = await _assignmentService.GetAssignmentsIncludeGroups();
			 return View(assignments);

		
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
   
        public async Task<IActionResult> Create(AssignmentDto assignment)
		{
           
            if (ModelState.IsValid)
			{
				await _assignmentService.Create(assignment);
				await _unitOfWork.Complete();
				return RedirectToAction(nameof(GetAllAssignments));
			}

            var selectedGroup = new GroupDto() { Id = 0, Title = "Select Group" };
            var allGroups = _groupService.GetAll().Result.ToList();
            allGroups.Add(selectedGroup);
            ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
                , assignment.GroupId);
            return View(assignment);
		}

  
        public async Task<IActionResult> Edit(int? id)
		{

			if (id is null || !_assignmentService.Any().Result)
				return NotFound();
			var assignment = await _assignmentService.GetById(id.Value);
			var allGroups = _groupService.GetAll().Result.ToList();
			ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
				, assignment.GroupId);
			return View(assignment);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Edit(AssignmentDto assignment)
		{
			if (assignment is null || !_assignmentService.Any().Result)
				return NotFound();
			if(ModelState.IsValid)
			{
				try
				{
					_assignmentService.Update(assignment);
					await _unitOfWork.Complete();
				}
				catch (DBConcurrencyException)
				{
					if (!_assignmentService.Contains(assignment).Result)
						return NotFound();
					throw;
				}
				return RedirectToAction(nameof(GetAllAssignments));
			}
			return View(assignment);
		}

   
        public async Task<IActionResult> Delete(int? id)
		{
			if (id is null || !_assignmentService.Any().Result)
				return NotFound();
			var assignment = await _assignmentService.GetById(id.Value);
			return View(assignment);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
   
        public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (!_assignmentService.Any().Result)
				return NotFound();

			_assignmentService.Delete(id);
			await _unitOfWork.Complete();
			return RedirectToAction(nameof(GetAllAssignments));
		}

        [AllowAnonymous]
        public async Task<IActionResult> GetAssignmentsAccordingSpecificStudent(StudentDto student)
		{
			var assignments = await _assignmentService.GetAssignmentsAccordingSpecificStudent(student.GroupId);
			ViewData["Student"] = student;
			var grades = await _evaluationGradeService.GetAll();
			ViewData["Grades"] = grades;
			return View(assignments);
		}
	}
}
