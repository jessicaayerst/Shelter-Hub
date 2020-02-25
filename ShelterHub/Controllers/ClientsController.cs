using System;
using System.Collections.Generic;
using System.IO;
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


        [Authorize]
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
            {//method for the user to search for a specific client by any string found within a client's first of last names
                clients = clients.Where(c => c.LastName.Contains(searchString)
                                       || c.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {//method that sorts the list of clients by descending alphabetical last name or by descending intake date(most recent date will be at the bottom, and furthest date will be at the top of list)
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
        {//method to Get a specific client by id. If the id is null, the query will return NotFound()
            if (id == null)
            {
                return NotFound();
            }
            //Get the current user
            var user = await GetCurrentUserAsync();
            //Create a variable named "client", that will include the current user, client steps, client groups, and returns the first client that matches both the parameter "id" and the current user's id
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
            //return the newly create variable "client"'s details
            return View(client);
        }
        [Authorize]
        // GET: Clients/Create
        public IActionResult Create()
        {//instantiate view model, that allows the client's image to be displayed on the view
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
            { //If the model is valid, then the client will be saved to the current user's list of clients, and the client will have a userId of the current user. The new client will be added to the list of clients.
                var currentUser = await GetCurrentUserAsync();
                if (vm.ImageFile != null)
                {//Method where if the image file contains an image, then that image will be displayed with the view
                    using (var memoryStream = new MemoryStream())
                    {
                        await vm.ImageFile.CopyToAsync(memoryStream);
                        vm.Client.ClientImage = memoryStream.ToArray();
                    }
                };

                //Set the new client's userId to the current user's id and add it the the current user's list of clients
                vm.Client.UserId = currentUser.Id;
                _context.Add(vm.Client);

                await _context.SaveChangesAsync();
                //After saving the new client, the user will be directed back to the Clients Index view
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

            var client = await _context.Clients
                .FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }
            //instantiate view model, where the vm's client is the client whose id matches the parameter "id"
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
           var clientWithImage = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            if (id != viewModel.Client.Id)
            {//if the parameter "id" does not matche the vm's client id, then return Not Found()
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {//if the model state is valid, get the current user

                    var currentUser = await GetCurrentUserAsync();
                    //if the vm's image file is null and the clientWithImage's image file is not null, then set the vm's image file equal to the ClientWithImage's image file, other wise, set the image file to the vm's image file. This conditional ensures that if the client already has an image, and the user doesn't change or add another image during the edit, that the original image will still be in the database, instead of nothing.
                    if (viewModel.ImageFile == null && clientWithImage.ClientImage != null)
                    {
                        viewModel.Client.ClientImage = clientWithImage.ClientImage;
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await viewModel.ImageFile.CopyToAsync(memoryStream);
                            viewModel.Client.ClientImage = memoryStream.ToArray();
                       }
                  }                 
                    ;
                    viewModel.Client.UserId = currentUser.Id;
                    _context.Update(viewModel.Client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(viewModel.Client.Id))
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
        {//sends user to a delete client confirmation page, if the user chooses to delete, then the client whose id matches the parameter "id" and whose userId matches the current user Id, will be deleted
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
        {//private method to check that the client actually exists
            return _context.Clients.Any(e => e.Id == id);
        }


        // GET: Clients/ClientWithSteps/5
        public async Task<IActionResult> ClientWithSteps(int? id)
        {//a seperate method that sends the user to a view that shows the client's list of steps.
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
    }
}
