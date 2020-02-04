using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShelterHub.Data;
using ShelterHub.Models;
using ShelterHub.Models.ViewModels;

namespace ShelterHub.Controllers
{
    public class StepsController : Controller
    {
        // Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // Inject user manager into constructor
        public StepsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [Authorize]
        // GET: Steps
        public async Task<IActionResult> Index()
        {
            ApplicationUser loggedInUser = await GetCurrentUserAsync();

            var applicationDbContext = _context.Steps.Include(s => s.StepType).Include(s => s.User).Where(s => s.User == loggedInUser);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Steps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            var step = await _context.Steps
                .Include(s => s.StepType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.User == user);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }
        [Authorize]
        // GET: Steps/Create
        public async Task<IActionResult> Create()
        {

            List<StepType> stepTypes = await _context.StepTypes.ToListAsync();

            var viewModel = new CreateStepsViewModel()
            {
                StepTypeOptions = stepTypes.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StepTypeName
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: Steps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStepsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await GetCurrentUserAsync();
                vm.Step.UserId = currentUser.Id;
                _context.Add(vm.Step);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the post fails, rebuild the view model and send it back to the view
            List<StepType> stepTypes = await _context.StepTypes.ToListAsync();

            vm.StepTypeOptions = stepTypes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.StepTypeName
            }).ToList();

            return View(vm);
        }

        // GET: Steps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Steps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            var stepTypes = await _context.StepTypes.ToListAsync();

            var viewModel = new EditStepsViewModel()
            {
                Step = step,
                StepTypeOptions = stepTypes.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StepTypeName
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Steps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditStepsViewModel viewModel)
        {
            var step = viewModel.Step;
            if (id != step.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();
                    step.UserId = currentUser.Id;
                    _context.Update(step);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StepExists(step.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var stepTypes = await _context.StepTypes.ToListAsync();
            viewModel.StepTypeOptions = stepTypes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.StepTypeName
            }).ToList();

            return View(viewModel);
        }

        // GET: Steps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var step = await _context.Steps
                .Include(s => s.StepType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (step == null)
            {
                return NotFound();
            }

            return View(step);
        }

        // POST: Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var step = await _context.Steps.FindAsync(id);
            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StepExists(int id)
        {
            return _context.Steps.Any(e => e.Id == id);
        }
    }
}
