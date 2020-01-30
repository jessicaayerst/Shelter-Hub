using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShelterHub.Data;
using ShelterHub.Models;
using ShelterHub.Models.ViewModels;

namespace ShelterHub.Controllers
{
    public class ClientsController : Controller
    {
        //Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // Inject user manager into constructor
        public ClientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        // GET: Clients
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ApplicationUser loggedInUser = await GetCurrentUserAsync();

            //The below method will only return clients of the currently logged in User

            List<Client> clientsOfUSer = await _context.Clients.Include(c => c.User).Where(c => c.User == loggedInUser).ToListAsync();



            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //The var clients will only bring back clients that have the currently logged in user's id.

            var clients = from c in clientsOfUSer select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.LastName.Contains(searchString)
                                       || c.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(c => c.LastName);
                    break;
                case "Date":
                    clients = clients.OrderBy(c => c.IntakeDate);
                    break;
                case "date_desc":
                    clients = clients.OrderByDescending(c => c.IntakeDate);
                    break;
                default:
                    clients = clients.OrderBy(c => c.LastName);
                    break;
            }
            return View(clients.ToList());

        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var client = await _context.Clients
                .Include(c => c.User)
                .Include(c => c.ClientSteps)
                .ThenInclude(ClientSteps => ClientSteps.Step)
                .Include(c => c.ClientGroups)
                .ThenInclude(ClientGroups => ClientGroups.Group)
                .FirstOrDefaultAsync(m => m.Id == id && m.User == user);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            CreateClientWithImageViewModel vm = new CreateClientWithImageViewModel();
          
            return View(vm);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientWithImageViewModel vm)
        {
            ModelState.Remove("client.UserId");
            if (ModelState.IsValid)
            {
                var currentUser = await GetCurrentUserAsync();
                if (vm.ImageFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await vm.ImageFile.CopyToAsync(memoryStream);
                        vm.Client.ClientImage = memoryStream.ToArray();
                    }
                };


                vm.Client.UserId = currentUser.Id;
                _context.Add(vm.Client);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }          
           
            return View(vm);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            var viewModel = new EditClientWithImageViewModel()
            {
                Client = client
            };
            return View(viewModel);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditClientWithImageViewModel viewModel)
        {
            var client = viewModel.Client;
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await GetCurrentUserAsync();

                    if (viewModel.ImageFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.ImageFile.CopyToAsync(memoryStream);
                            viewModel.Client.ClientImage = memoryStream.ToArray();
                        }
                    };
                    client.UserId = currentUser.Id;
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            else
            {

            }
            return View(viewModel);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
