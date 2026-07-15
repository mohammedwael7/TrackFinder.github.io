using Microsoft.EntityFrameworkCore;
using TrackFinder.Context;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Services.AuthServices.Helpers
{
    public class UserFunctions : IUserFunctions
    {
        private readonly AppDbContext _db;

        public UserFunctions(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
            => await _db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());

        public async Task<bool> IsUsernameTakenAsync(string username)
            => await _db.Users.AnyAsync(u => u.UserName!.ToLower() == username.ToLower());

        public async Task<bool> IsEmailVerifiedAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            return user?.EmailConfirmed ?? false;
        }

        public async Task<bool> IsUserBannedAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            return user?.IsBanned ?? false;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _db.Users
                        .Include(u => u.Student)
                        .Include(u => u.Instructor)
                        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}