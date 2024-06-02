using LearnAsp.Domain;
using LearnAsp.Exceptions;
using LearnAsp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnAsp.Controllers
{
    [Authorize]
    public class VisitController : Controller
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IVisitRepository _visitRepository;

        public VisitController(IProblemRepository problemRepository, IVisitRepository visitRepository)
        {
            _problemRepository = problemRepository;
            _visitRepository = visitRepository;
        }

        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Index()
        {
            var items = await _visitRepository.GetAllAsync(new Guid(HttpContext.Request.Headers["uid-user"]));
            return View(items);
        }

        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Create()
        {
            var problems = await _problemRepository.GetAllAsync();
            ViewBag.Problems = new SelectList(problems, "Id", "Name");
            return View();
        }

        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var visit = await _visitRepository.GetByIdAsync(id);
                return View(visit);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var visit = await _visitRepository.GetByIdAsync(id);
                var problems = await _problemRepository.GetAllAsync();
                ViewBag.Problems = new SelectList(problems, "Id", "Name");
                return View(visit);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Create(Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return View(visit);
            }
            visit.UserId = new Guid(HttpContext.Request.Headers["uid-user"]);
            await _visitRepository.AddAsync(visit);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "RequireLoggedIn")]
        public async Task<IActionResult> Edit(int id, Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return View(visit);
            }
            await _visitRepository.UpdateAsync(visit);
            return RedirectToAction("Index");
        }
    }
}
