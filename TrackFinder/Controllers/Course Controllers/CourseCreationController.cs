using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.CourseModels;

namespace TrackFinder.Controllers;

public class CourseCreationController : Controller
{
	private readonly AppDbContext _context;

	public CourseCreationController(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> Index()
	{
		var courses = await _context.Courses
			.AsNoTracking()
			.ToListAsync();

		return View(courses);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		ViewBag.GainedSkills = await _context.GainedSkills
			.AsNoTracking()
			.ToListAsync();

		return View();
	}

	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Create(Course request, List<int> selectedSkillIds)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.GainedSkills = await _context.GainedSkills
			.AsNoTracking()
			.ToListAsync();
			return View(request);
		}

		request.CourseSkills = (selectedSkillIds ?? [])
			.Distinct()
			.Select(skillId => new CourseSkill { GainedSkillId = skillId })
			.ToList();

		await _context.Courses.AddAsync(request);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		var course = await _context.Courses
			.FirstOrDefaultAsync(c => c.Id == id);

		if (course is null)
			return NotFound();

		_context.Courses.Remove(course);
		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Details(Guid id)
	{
		var course = await _context.Courses
			.Include(c => c.Lessons)
			.Include(c => c.CourseSkills)
				.ThenInclude(cs => cs.GainedSkill)
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.Id == id);

		if (course is null)
			return NotFound();

		return View(course);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(Guid id)
	{
		var course = await _context.Courses
			.Include(c => c.CourseSkills)
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.Id == id);

		if (course is null)
			return NotFound();

		ViewBag.GainedSkills = await _context.GainedSkills
			.AsNoTracking()
			.ToListAsync();

		ViewBag.SelectedSkillIds = course.CourseSkills
			.Select(cs => cs.GainedSkillId)
			.ToList();

		return View(course);
	}

	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Edit(Guid id, Course request, List<int> selectedSkillIds)
	{
		if (!ModelState.IsValid)
		{
			ViewBag.GainedSkills = await _context.GainedSkills
				.AsNoTracking()
				.ToListAsync();

			ViewBag.SelectedSkillIds = selectedSkillIds;

			return View(request);
		}

		var course = await _context.Courses
			.Include(c => c.CourseSkills)
			.FirstOrDefaultAsync(c => c.Id == id);

		if (course is null)
			return NotFound();

		course.MapFrom(request);

		var distinctSkillIds = (selectedSkillIds ?? []).Distinct().ToHashSet();

		var skillsToRemove = course.CourseSkills
			.Where(cs => !distinctSkillIds.Contains(cs.GainedSkillId))
			.ToList();

		foreach (var skill in skillsToRemove)
			course.CourseSkills.Remove(skill);

		var existingSkillIds = course.CourseSkills
			.Select(cs => cs.GainedSkillId)
			.ToHashSet();

		foreach (var skillId in distinctSkillIds.Where(sId => !existingSkillIds.Contains(sId)))
			course.CourseSkills.Add(new CourseSkill { CourseId = course.Id, GainedSkillId = skillId });

		await _context.SaveChangesAsync();

		return RedirectToAction(nameof(Details), new { id });
	}
}
