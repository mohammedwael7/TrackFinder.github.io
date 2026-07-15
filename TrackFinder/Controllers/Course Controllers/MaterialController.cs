using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.CourseModels;
using TrackFinder.ViewModels;

namespace TrackFinder.Controllers;

public class MaterialController : Controller
{
	private readonly AppDbContext _context;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public MaterialController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
	{
		_context = context;
		_webHostEnvironment = webHostEnvironment;
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var material = await _context.Materials
			.FindAsync(id);

		if (material is null)
			return NotFound();

		return View(material);
	}

	[HttpGet]
	public async Task<IActionResult> Create(Guid id)
	{
		var lessonExists = await _context.Lessons
			.AnyAsync(l => l.Id == id);

		if (!lessonExists)
			return NotFound();

		var model = new MaterialUploadVM { LessonId = id };

		return View(model);
	}

	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Create(MaterialUploadVM model)
	{
		if (!ModelState.IsValid)
			return View(model);

		var lessonExists = await _context.Lessons
			.AnyAsync(m => m.Id == model.LessonId);

		if (!lessonExists)
			return NotFound();

		if (model.File == null || model.File.Length == 0)
		{
			ModelState.AddModelError("File", "Please select a file to upload.");
			return View(model);
		}

		var extension = Path.GetExtension(model.File.FileName).ToLower();
		if (extension != ".rar" && extension != ".zip")
		{
			ModelState.AddModelError("File", "Only .rar or .zip files are allowed.");
			return View(model);
		}

		string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "materials");
		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
		string filePath = Path.Combine(uploadsFolder, uniqueFileName);

		using (var fileStream = new FileStream(filePath, FileMode.Create))
		{
			await model.File.CopyToAsync(fileStream);
		}

		var material = new Material
		{
			Id = Guid.NewGuid(),
			FileName = model.File.FileName,
			FileUrl = "/materials/" + uniqueFileName,
			ContentType = model.File.ContentType,
			LessonId = model.LessonId
		};

		_context.Materials.Add(material);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Details), new { id = material.Id });
	}

	[HttpGet]
	public async Task<IActionResult> Download(Guid id)
	{
		var material = await _context.Materials.FindAsync(id);

		if (material == null)
			return NotFound();

		return View(material);
	}

	[HttpPost, ActionName("Download")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DownloadConfirmed(Guid id)
	{
		var material = await _context.Materials.FindAsync(id);

		if (material == null)
			return NotFound();

		var filePath = Path.Combine(_webHostEnvironment.WebRootPath, material.FileUrl.TrimStart('/'));

		if (!System.IO.File.Exists(filePath))
			return NotFound("File not found on the server.");

		byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
		return File(fileBytes, material.ContentType, material.FileName);
	}
}
