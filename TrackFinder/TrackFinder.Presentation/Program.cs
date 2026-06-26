using Microsoft.EntityFrameworkCore;
using TrackFinderDb.Models.TrackFinderDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new ArgumentNullException($"connection string is empty");
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
