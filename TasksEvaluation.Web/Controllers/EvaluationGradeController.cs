using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskEvaluation.InfraStructure.Repositories.Services;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Web.ViewModels;

namespace TasksEvaluation.Web.Controllers
{
    [Authorize]
    public class EvaluationGradeController : Controller
    {
        private readonly IEvaluationGradeService _evaluationGradeService;
		private readonly ISolutionService _solutionService;

		private readonly IUnitOfWork _unitOfWork;
        public EvaluationGradeController(IEvaluationGradeService evaluationGradeService , ISolutionService solutionService , IUnitOfWork unitOfWork)
        {
            _evaluationGradeService = evaluationGradeService;
			_solutionService = solutionService;
			_unitOfWork = unitOfWork;

        }
        public async Task<IActionResult> EvaluateSolution(int? solutionId )
        {
            var solution = await _solutionService.GetById(solutionId.Value);
            var grades =  _evaluationGradeService.GetAll().Result.ToList();
            var selectedGrade = new EvaluationGradeDto() { Id = 0, Grade = "Select Grade" };
            grades.Add(selectedGrade);
            ViewBag.GradeId = new SelectList(grades, nameof(EvaluationGradeDto.Id), nameof(EvaluationGradeDto.Grade),
                solution.GradeId ?? 0);
            
            ViewData["solution"] = solution;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task< IActionResult> EvaluateSolution(SolutionGradeVM solutionGradeVM)
        {
            var solution = await _solutionService.GetById(solutionGradeVM.SolutionId.Value);

            if (solutionGradeVM.GradeId != 0)
            {
                solution.GradeId = solutionGradeVM.GradeId;
                _solutionService.Update(solution);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(SolutionController.GetAllSolutions), "Solution");
            }
            ModelState.AddModelError(string.Empty, "Please Choose Grade");
			var grades = _evaluationGradeService.GetAll().Result.ToList();
			var selectedGrade = new EvaluationGradeDto() { Id = 0, Grade = "Select Grade" };
			grades.Add(selectedGrade);
			ViewBag.GradeId = new SelectList(grades, nameof(EvaluationGradeDto.Id), nameof(EvaluationGradeDto.Grade),
				solution.GradeId ?? 0);

			ViewData["solution"] = solution;
			return View(solutionGradeVM);
		}

        public async Task<IActionResult> ShowGrade(int solutionId)
        {
            var solution = await _solutionService.GetById(solutionId);
            if (solution is null)
                return NotFound();
 

            ViewBag.Grade = solution.GradeId is null ? "Please Waiting , Your Solution Under Evaluation."
                                                      : _evaluationGradeService.GetById(solution.GradeId.Value).Result.Grade;
            return View(solution) ;

		}
	}
}
