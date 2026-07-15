using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;
using TrackFinder.ViewModels.Users;

namespace TrackFinder.Controllers.Admin;

public class UsersController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly PasswordHasher<User> _passwordHasher = new();

    public UsersController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // ========================= INDEX =========================

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users
            .Select(u => new UserListVM
            {
                Id = u.Id,
                Username = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        return View("~/Views/Admin/Users/Index.cshtml", users);
    }

    // ========================= DETAILS =========================

    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
            return NotFound();

        var model = new UserDetailsVM
        {
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Birthdate = user.Birthdate,
            Gender = user.Gender,
            Bio = user.Bio,
            ProfilePictureUrl = user.ProfilePictureUrl,
            EmailVerified = user.EmailVerified,
            IsBanned = user.IsBanned,
            CreatedAt = user.CreatedAt,
            Role = user.Role
        };

        return View("~/Views/Admin/Users/Details.cshtml", model);
    }

    // ========================= CREATE =========================

    [HttpGet]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Users/Create.cshtml",
            new CreateUserVM
            {
                Birthdate = DateTime.Today
            });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserVM model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Users/Create.cshtml", model);

        if (await _context.Users.AnyAsync(x => x.Email == model.Email))
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return View("~/Views/Admin/Users/Create.cshtml", model);
        }

        if (await _context.Users.AnyAsync(x => x.UserName == model.Username))
        {
            ModelState.AddModelError("Username", "Username already exists.");
            return View("~/Views/Admin/Users/Create.cshtml", model);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Birthdate = model.Birthdate,
            Gender = model.Gender,
            Bio = model.Bio,
            EmailVerified = model.EmailVerified,
            IsBanned = model.IsBanned,
            Role = model.Role,
            CreatedAt = DateTime.UtcNow
        };

        // Hash Password
        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

        if (model.Image != null && model.Image.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.Image.CopyToAsync(stream);

            user.ProfilePictureUrl = "/uploads/" + fileName;
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // ========================= EDIT =========================

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        var model = new EditUserVM
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Birthdate = user.Birthdate,
            Gender = user.Gender,
            Bio = user.Bio,
            EmailVerified = user.EmailVerified,
            IsBanned = user.IsBanned,
            Role = user.Role
        };

        return View("~/Views/Admin/Users/Edit.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EditUserVM model)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View("~/Views/Admin/Users/Edit.cshtml", model);

        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        if (await _context.Users.AnyAsync(x => x.Email == model.Email && x.Id != id))
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return View("~/Views/Admin/Users/Edit.cshtml", model);
        }

        if (await _context.Users.AnyAsync(x => x.UserName == model.Username && x.Id != id))
        {
            ModelState.AddModelError("Username", "Username already exists.");
            return View("~/Views/Admin/Users/Edit.cshtml", model);
        }

        user.UserName = model.Username;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Birthdate = model.Birthdate;
        user.Gender = model.Gender;
        user.Bio = model.Bio;
        user.EmailVerified = model.EmailVerified;
        user.IsBanned = model.IsBanned;
        user.Role = model.Role;

        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
        }

        if (model.Image != null && model.Image.Length > 0)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid() + Path.GetExtension(model.Image.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await model.Image.CopyToAsync(stream);

            user.ProfilePictureUrl = "/uploads/" + fileName;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // ========================= DELETE =========================

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        var model = new UserDetailsVM
        {
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Birthdate = user.Birthdate,
            Gender = user.Gender,
            Bio = user.Bio,
            ProfilePictureUrl = user.ProfilePictureUrl,
            EmailVerified = user.EmailVerified,
            IsBanned = user.IsBanned,
            CreatedAt = user.CreatedAt,
            Role = user.Role
        };

        return View("~/Views/Admin/Users/Delete.cshtml", model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}