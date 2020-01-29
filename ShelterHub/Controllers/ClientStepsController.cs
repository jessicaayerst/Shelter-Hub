using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShelterHub.Data;
using ShelterHub.Models;
using ShelterHub.Models.ViewModels;

namespace ShelterHub.Controllers
{
    public class ClientStepsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientStepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientSteps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientSteps.Include(c => c.Client).Include(c => c.Step);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientSteps/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ClientSteps/Create
        public async Task<IActionResult> Create(int id)
        {
            List<Client> clients = await _context.Clients.ToListAsync();

            var step = await _context.Steps.Include(s => s.ClientSteps).FirstOrDefaultAsync(m => m.Id == id);
            var viewModel = new CreateClientStepViewModel()
            {

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
            {
                _context.Add(vm.ClientStep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the post fails, rebuild the view model and send it back to the view
            List<Client> clients = await _context.Clients.ToListAsync();

            vm.ClientNameOptions = clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToList();

            return View(vm);
        }

        // GET: ClientSteps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
            return View(clientStep);
        }

        // POST: ClientSteps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,StepId,DateStarted,DateCompleted")] ClientStep clientStep)
        {
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool ClientStepExists(int id)
        {
            return _context.ClientSteps.Any(e => e.Id == id);
        }
    }
}
