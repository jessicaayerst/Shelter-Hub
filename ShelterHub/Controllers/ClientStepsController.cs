using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShelterHub.Data;
using ShelterHub.Models;
using ShelterHub.Models.ViewModels;
using static ShelterHub.Models.ClientStep;

namespace ShelterHub.Controllers
{
    public class ClientStepsController : Controller
    {
       
        //Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // Inject user manager into constructor
        public ClientStepsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: ClientSteps
        public async Task<IActionResult> Index()
        {//The user is never directed to the ClientSteps Index view. If there was a link to this view, it would show a long list of every Client/Step relationship in the database.
            var applicationDbContext = _context.ClientSteps.Include(c => c.Client).Include(c => c.Step);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientSteps/Details/5
        public async Task<IActionResult> Details(int? id)
        {//This user is never directed to the ClientStep Details view. The user can see a list of steps that each client is enlisted in, on the the Client - ClientWithSteps View (a seperate view created in the Clients Controller for the purpose of listing the specific client's steps). The user can get to this view from the Client Details view, then clicking the "Go To Client's Steps" link.
            if (id == null)
            {
                return NotFound();
            }

            var clientStep = await _context.ClientSteps
                .Include(c => c.Client)
                .Include(c => c.Step)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientStep == null)
            {
                return NotFound();
            }

            return View(clientStep);
        }

        // GET: ClientSteps/Create
        public async Task<IActionResult> Create(int id)
        {//When the user clicks "Add Step to Client", from the Step Index view, or Client Details view, then the user will be directed to the ClientSteps Create View, which has been created using a view model.
            var user = await GetCurrentUserAsync();

            List<Client> clients = await _context.Clients
                .Include(c => c.User).Where(c => c.UserId == user.Id)
                .ToListAsync();

           

            var step = await _context.Steps
                 .Include(s => s.User)
                .Include(s => s.ClientSteps)
                .FirstOrDefaultAsync(m => m.Id == id && m.User == user);
            //instantiate view model
            var viewModel = new CreateClientStepViewModel()
            {
                //display a drop down list of clients where the user will be able to select a client. Once the client is selected, a ClientStep will be created, which will have the id of that specific client, and the id of the specific step.
                ClientNameOptions = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName
                }).ToList(),
                ClientStep = new ClientStep()
                {
                    StepId = id
                }
            };
            
            ViewData["StepId"] = step.StepName;
            return View(viewModel);
        }

        // POST: ClientSteps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientStepViewModel vm)
        {
            

            if (ModelState.IsValid)
            {//if the model state is valid, the new ClientStep will be added to the database
                var ListOfClientSteps = _context.ClientSteps;
                //This conditional just makes sure that the client can only be added to this particular step once. If there is already a ClientStep, where the client id and the step id both match the ones just selected by the user, then the user will simply be returned to the Client Details page for that client, instead of creating a duplicate ClientStep.
                if (!ListOfClientSteps.Any(cs => cs.ClientId == vm.ClientStep.ClientId && cs.StepId == vm.ClientStep.StepId))
                {
                    _context.Add(vm.ClientStep);
                    await _context.SaveChangesAsync();
                    //Direct the user to the Client Details page of the client who was chosen in the drop down box.
                    return RedirectToAction("Details", "Clients", new { id = vm.ClientStep.ClientId });
                }
                else
                { //Direct the user to the Client Details page of the client who was chosen in the drop down box.
                    return RedirectToAction("Details", "Clients", new { id = vm.ClientStep.ClientId });
                }
               
            }

            // If the post fails, rebuild the view model and send it back to the view
            List<Client> clients = await _context.Clients.ToListAsync();
            //display a drop down list of clients where the user will be able to select a client. Once the client is selected, a ClientStep will be created, which will have the id of that specific client, and the id of the specific step.
            vm.ClientNameOptions = clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToList();

            return View(vm);
        }

        // GET: ClientSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {//the user will only go to the ClientStep Edit view to change the start date or end date of the step. Nothing else can be changed in the edit view.
            if (id == null)
            {
                return NotFound();
            }

            var clientStep = await _context.ClientSteps.Include(c => c.Client).Include(c => c.Step).FirstOrDefaultAsync(m => m.Id == id); 
            if (clientStep == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = clientStep.Client.FullName;
            ViewData["StepId"] = clientStep.Step.StepName;
            ViewData["DateStarted"] = clientStep.DateStarted;
            return View(clientStep);
        }

        // POST: ClientSteps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,StepId,DateStarted,DateCompleted")] ClientStep clientStep)
        {//If the user changes the start or end date of a ClientStep, then the updates will be saved to the database.
            if (id != clientStep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientStep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientStepExists(clientStep.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ClientWithSteps", "Clients", new { id = clientStep.ClientId });
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientStep.ClientId);
            ViewData["StepId"] = new SelectList(_context.Steps, "Id", "Id", clientStep.StepId);
            return View(clientStep);
        }

        // GET: ClientSteps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientStep = await _context.ClientSteps
                .Include(c => c.Client)
                .Include(c => c.Step)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientStep == null)
            {
                return NotFound();
            }

            return View(clientStep);
        }

        // POST: ClientSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientStep = await _context.ClientSteps.FindAsync(id);
            _context.ClientSteps.Remove(clientStep);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = clientStep.ClientId });
        }

        private bool ClientStepExists(int id)
        {
            return _context.ClientSteps.Any(e => e.Id == id);
        }
       
    }
}
