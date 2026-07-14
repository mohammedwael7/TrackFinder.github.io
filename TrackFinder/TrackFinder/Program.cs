using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackFinder.Models.TrackFinderDbContext;
using TrackFinder.Models.UserModels;
using TrackFinder.Providers.Common.EmailService;
using TrackFinder.Providers.Private.AuthProviders;
using TrackFinder.Services.AuthServices.Helpers;
using TrackFinder.Services.AuthServices.Implementations;
using TrackFinder.Services.AuthServices.Interfaces;
using TrackFinder.Services.UserProfileServices;

namespace TrackFinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ── EF Core ───────────────────────────────────────────────────────────
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ── Identity ──────────────────────────────────────────────────────────
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Configure Identity Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Login/Logout";
                options.AccessDeniedPath = "/Login";

                options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;

                options.Cookie.Name = "tf_auth";
            });

            // ── MemoryCache ──────────────────────────────────────────────────────────
            builder.Services.AddMemoryCache();

            // ── MVC ───────────────────────────────────────────────────────────────
            builder.Services.AddControllersWithViews();

            // ── Dependency Injection — Providers ──────────────────────────────────
            builder.Services.AddScoped<IEncryptPasswordProvider, EncryptPasswordProvider>();
            builder.Services.AddScoped<ITokenProvider,           TokenProvider>();
            builder.Services.AddScoped<IEmailSender,             EmailSender>();

            // ── Dependency Injection — Service Helpers ────────────────────────────
            builder.Services.AddScoped<IUserFunctions, UserFunctions>();

            // ── Dependency Injection — Auth Services ──────────────────────────────
            builder.Services.AddScoped<ILoginService,         LoginService>();
            builder.Services.AddScoped<IRegistrationService,  RegistrationService>();
            builder.Services.AddScoped<IOtpService,           OtpService>();
            builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
            builder.Services.AddScoped<IUserProfileService,   UserProfileService>();

            var app = builder.Build();

            // ── Pipeline ──────────────────────────────────────────────────────────
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // IMPORTANT: Authentication MUST come before Authorization
            app.UseAuthentication();   // ← reads cookie → populates User.Identity
            app.UseAuthorization();    // ← enforces [Authorize] attributes

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
