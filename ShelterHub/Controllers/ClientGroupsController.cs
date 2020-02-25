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
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace ShelterHub.Controllers
{
    public class ClientGroupsController : Controller
    {
        //Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // Inject user manager into constructor
        public ClientGroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: ClientGroups
        public async Task<IActionResult> Index()
        {//The user is never directed to the ClientGroup Index view. If there was a link to this view, it would show a long list of every Client/Group relationship in the database.
            var applicationDbContext = _context.ClientGroups.Include(c => c.Client).Include(c => c.Group);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClientGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {//This user is never directed to the ClientGroup Details view. The user can see a list of groups that each client is enlisted in, on the the Client Details view. There was no need to show this specific view in the app.
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
            //When the user clicks "Add Group to Client", from the Group Index view, or Group Details view, then the user will be directed to the ClientGroups Create View, which has been created using a view model.
            var user = await GetCurrentUserAsync();

            List<Client> clients = await _context.Clients
                 .Include(c => c.User).Where(c => c.UserId == user.Id)
                 .ToListAsync();

            

            var group = await _context.Groups
                .Include(c => c.User)
                .Include(g => g.ClientGroups)
                .FirstOrDefaultAsync(m => m.Id == id && m.User == user);
            //instantiate view model
            var viewModel = new CreateClientGroupViewModel()
            {
                //display a drop down list of clients where the user will be able to select a client. Once the client is selected, a ClientGroup will be created, which will have the id of that specific client, and the id of the specific group.
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
            {//if the model state is valid, the new ClientGroup will be added to the database
                _context.Add(vm.ClientGroup);
               
            await _context.SaveChangesAsync();
                //Direct the user to the Client Details page of the client who was chosen in the drop down box.
                return RedirectToAction("Details", "Clients", new { id = vm.ClientGroup.ClientId });
            }

            // If the post fails, rebuild the view model and send it back to the view
            List<Client> clients = await _context.Clients.ToListAsync();
            //display a drop down list of clients where the user will be able to select a client. Once the client is selected, a ClientGroup will be created, which will have the id of that specific client, and the id of the specific group.
            vm.ClientNameOptions = clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToList();

            return View(vm);
        }

        // GET: ClientGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {//The user is never directed to the ClientGroup Edit view
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
