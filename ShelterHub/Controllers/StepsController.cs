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
        {//Gets the steps of the current user
            ApplicationUser loggedInUser = await GetCurrentUserAsync();
            //Creates a new variable that includes the step type of the steps, where the step's UserId matches the current user's Id
            var applicationDbContext = _context.Steps.Include(s => s.StepType).Include(s => s.User).Where(s => s.User == loggedInUser);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Steps/Details/5
        public async Task<IActionResult> Details(int? id)
        {//Gets the details of the step whose id matches the parameter "id" and the current user's id matches the step's UserId
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
            //create a list called stepTypes and add the StepTypes from the db to the list
            List<StepType> stepTypes = await _context.StepTypes.ToListAsync();
            //instantiate view model
            var viewModel = new CreateStepsViewModel()
            {//set up drop down to display and select step type names
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
            {//if the model state is valid, and the currentUser's id matches the id of the specific step, add the new step to that user's list of steps
                var currentUser = await GetCurrentUserAsync();
                vm.Step.UserId = currentUser.Id;
                _context.Add(vm.Step);
                await _context.SaveChangesAsync();
                //return to the Steps Index view
                return RedirectToAction(nameof(Index));
            }

            // If the post fails, rebuild the view model and send it back to the view
            List<StepType> stepTypes = await _context.StepTypes.ToListAsync();
            //set up drop down to display and select step type names
            vm.StepTypeOptions = stepTypes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.StepTypeName
            }).ToList();

            return View(vm);
        }

        // GET: Steps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {//Method to edi the step whose id matched the parameter "id"
            if (id == null)
            {
                return NotFound();
            }
            //prepopulate the form fields with the database values that have already been saved for this specific step
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
            {//if the id isn't found, then return NOtFOund()
                return NotFound();
            }

            var stepTypes = await _context.StepTypes.ToListAsync();
            //instantiate view model
            var viewModel = new EditStepsViewModel()
            {//set up drop down to display and select step type names
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
            {//if the model state is valid, then the updates will be saved to the specific step's table in the database
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
        {//method that double checks if the user wants to permanently delete the step from the database
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
        {//method that deletes the step whose id matches the parameter "id" from the database and redirects the user back to the Steps Index view afterwards
            var step = await _context.Steps.FindAsync(id);
            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StepExists(int id)
        {//Private method to ensure that the step actually exists
            return _context.Steps.Any(e => e.Id == id);
        }
    }
}
