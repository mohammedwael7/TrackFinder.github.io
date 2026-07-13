using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Models.CourseModels;
using TrackFinderDb.Models.TrackFinderDbContext;

namespace TrackFinder.Controllers;

public class LessonController : Controller
{
	private readonly AppDbContext _context;

	public LessonController(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var lesson = await _context.Lessons
			.Include(l => l.Materials)
			.AsNoTracking()
			.FirstOrDefaultAsync(l => l.Id == id);

		if (lesson is null)
			return NotFound();

		return View(lesson);
	}

	[HttpGet]
	public async Task<IActionResult> Create(Guid id)
	{
		var courseExists = await _context.Courses
			.AnyAsync(c => c.Id == id);

		if (!courseExists)
			return NotFound();

		var lesson = new Lesson { CourseId = id };

		return View(lesson);
	}

	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Create(Lesson lesson)
	{
		if (!ModelState.IsValid)
			return View(lesson);

		var courseExists = await _context.Courses
			.AnyAsync(c => c.Id == lesson.CourseId);

		if (!courseExists)
			return NotFound();

		await _context.Lessons.AddAsync(lesson);
		await _context.SaveChangesAsync();

		return RedirectToAction("Index", "Course", new { id = lesson.CourseId });
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var lesson = await _context.Lessons
			.FindAsync(id);

		if (lesson is null)
			return NotFound();

		return View(lesson);
	}

	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Edit(Guid id, Lesson request)
	{
		if (!ModelState.IsValid)
			return View(request);

		var lesson = await _context.Lessons
			.FindAsync(id);

		if (lesson is null)
			return NotFound();

		lesson.MapFrom(request);

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Details), new { id });
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		var lesson = await _context.Lessons
			.FindAsync(id);

		if (lesson is null)
			return NotFound();

		_context.Lessons.Remove(lesson);
		await _context.SaveChangesAsync();

		return RedirectToAction("Details", "Course", new { id = lesson.CourseId });
	}
}
