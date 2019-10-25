using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity.Data;
using Identity.Data.Models;

namespace Identity.Database.Seeder
{
    public sealed class AppUserDataSeeder
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly JsonFileParser _seedFileParser;
        private readonly string _defaultPassword;
        private readonly string _appUserJsonFilePath;

        public AppUserDataSeeder(
            AppIdentityDbContext context,
            UserManager<AppUser> userManager,
            JsonFileParser seedFileParser,
            string defaultPassword,
            string appUserJsonFilePath)
        {
            _context = context
               ?? throw new ArgumentNullException(nameof(context));

            _userManager = userManager
               ?? throw new ArgumentNullException(nameof(userManager));

            _seedFileParser = seedFileParser
                ?? throw new ArgumentNullException(nameof(seedFileParser));

            if (string.IsNullOrWhiteSpace(defaultPassword))
                throw new ArgumentException("Default password is required.", nameof(defaultPassword));

            _defaultPassword = defaultPassword;

            if (string.IsNullOrWhiteSpace(appUserJsonFilePath))
                throw new ArgumentException("message", nameof(appUserJsonFilePath));

            _appUserJsonFilePath = appUserJsonFilePath;
        }

        public async Task SeedAsync()
        {
            if (_context.Users.Any())
                await DeleteAllUsersAsync();

            var users = await _seedFileParser
                .LoadDataFromFileAsync<AppUser>(_appUserJsonFilePath)
                ?? new AppUser[0];

            if (!users.Any())
                return;

            await CreateUsersAsync(users);
        }

        private async Task DeleteAllUsersAsync()
        {
            var usrs = await _context.Users.ToListAsync();

            foreach (var user in usrs)
                await _userManager.DeleteAsync(user);
        }

        private async Task CreateUsersAsync(IReadOnlyList<AppUser> users)
        {
            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, _defaultPassword);

                if (!result.Succeeded)
                {
                    var reasons = string.Join("", result.Errors.Select(e => e.Description).ToList());
                    throw new InvalidOperationException($"Create user failed. {reasons}.");
                }
            }

            _context.SaveChanges();
        }
    }
}
