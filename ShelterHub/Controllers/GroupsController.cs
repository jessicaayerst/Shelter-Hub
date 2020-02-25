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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ShelterHub.Controllers
{
    public class GroupsController : Controller
    {
        // Private field to store user manager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        // Inject user manager into constructor
        public GroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Private method to get current user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [Authorize]
        // GET: Groups
        public async Task<IActionResult> Index()
        {
            //Get current user, aka "loggedInUser"
            ApplicationUser loggedInUser = await GetCurrentUserAsync();
            //Get current user's Groups
            var applicationDbContext = _context.Groups.Include(g => g.User).Where(g => g.User == loggedInUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Only show the group whose id matches the parameter "id" and the user is the current user
            var user = await GetCurrentUserAsync();

            var @group = await _context.Groups
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.User == user);
            if (@group == null)
            {
                return NotFound();
            }
            //Return the specific group that matches both of the parameters mentioned in line 52
            return View(@group);
        }
        [Authorize]
        // GET: Groups/Create
        public IActionResult Create()
        {
            //This will be the generic Create form for Create New Group, which is made by Entity
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GroupName,MaxAttendees,Time,DayOfWeek,Place")] Group @group)
        {
            if (ModelState.IsValid)
            {
                //If the model is valid, then the group will be saved to the current user's list of groups, and the group will have a userId of the current user. The new group will be added to the list of groups.
                var currentUser = await GetCurrentUserAsync();
                @group.UserId = currentUser.Id;
                _context.Add(@group);
        //The Await context.saveChangesAsync() method avoid blocking a thread while the changes are written to the DB.
                await _context.SaveChangesAsync();
                //Send user back to the Groups Index page
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @group.UserId);
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {//the parameter "id" is the id of the group that the user chooses to edit. If the id in the browser is null, it will return NotFound()
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            //If the id matches a group id, then the edit form will prepopulate the form with the details that are already in the database
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @group.UserId);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GroupName,MaxAttendees,Time,DayOfWeek,Place")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {//If the model state is valid, then get the current user and then get then update the group details for that user's group
                    var currentUser = await GetCurrentUserAsync();
                    @group.UserId = currentUser.Id;
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return to the Groups Index view 
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", @group.UserId);
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {//When the user clicks on delete for a specific group, that group's id is found in the database, checked against the current user's id, and removed
            if (id == null)
            {
                return NotFound();
            }
           
            var @group = await _context.Groups
                .Include(g => g.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {//Once the user confirms they would like to delete the group, the group's details, id, and all other information is removed from the database and will no longer show up in the user's list of groups
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {//Private method to ensure that a group exists before updating it in the database
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
