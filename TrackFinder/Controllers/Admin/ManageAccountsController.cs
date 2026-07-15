using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Controllers.Admin;

public class ManageAccountsController : Controller
{
    private readonly AppDbContext _context;

    public ManageAccountsController(AppDbContext context)
    {
        _context = context;
    }

   
    public async Task<IActionResult> Index()
    {
        var requests = await _context.Users
            .Include(u => u.Instructor)
            .Where(u => u.Role == UserRole.Instructor && !u.EmailVerified)
            .OrderByDescending(u => u.CreatedAt)
            .ToListAsync();

        return View("~/Views/Admin/ManageAccounts/Index.cshtml", requests);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        user.EmailVerified = true;

        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        TempData["Success"] = "Instructor account approved successfully.";

        return RedirectToAction(nameof(Index));
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Instructor)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound();

        if (user.Instructor != null)
        {
            _context.Instructors.Remove(user.Instructor);
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        TempData["Success"] = "Instructor account rejected.";

        return RedirectToAction(nameof(Index));
    }
}