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
    public class ClientGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClientGroups.Include(c => c.Client).Include(c => c.Group);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups
                .Include(c => c.Client)
                .Include(c => c.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientGroup == null)
            {
                return NotFound();
            }

            return View(clientGroup);
        }

        // GET: ClientGroups/Create
        public async Task<IActionResult> Create(int id)
        {


            List<Client> clients = await _context.Clients.ToListAsync();

            var group = await _context.Groups.Include(g => g.ClientGroups).FirstOrDefaultAsync(m => m.Id == id);
            var viewModel = new CreateClientGroupViewModel()
            {

                ClientNameOptions = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName
                }).ToList(),
                ClientGroup = new ClientGroup()
                {
                    GroupId = id
                }
            };

            ViewData["GroupId"] = group.GroupName;
            return View(viewModel);
        }

        // POST: ClientGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vm.ClientGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups");
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

        // GET: ClientGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups.FindAsync(id);
            if (clientGroup == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientGroup.ClientId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", clientGroup.GroupId);
            return View(clientGroup);
        }

        // POST: ClientGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,GroupId")] ClientGroup clientGroup)
        {
            if (id != clientGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientGroupExists(clientGroup.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientGroup.ClientId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", clientGroup.GroupId);
            return View(clientGroup);
        }

        // GET: ClientGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientGroup = await _context.ClientGroups
                .Include(c => c.Client)
                .Include(c => c.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientGroup == null)
            {
                return NotFound();
            }

            return View(clientGroup);
        }

        // POST: ClientGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientGroup = await _context.ClientGroups.FindAsync(id);
            _context.ClientGroups.Remove(clientGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Groups");
        }

        private bool ClientGroupExists(int id)
        {
            return _context.ClientGroups.Any(e => e.Id == id);
        }
    }
}
