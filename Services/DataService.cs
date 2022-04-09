using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Data;
using TheBlogProject.Enums;
using TheBlogProject.Models;

namespace TheBlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Task Create the DB fcrom the migrations
            await _dbContext.Database.MigrateAsync();

            await SeedRolesAsync();

            //Task 2: Seed a few users into the system
            await SeedUserAsync();
        }


        private async Task SeedRolesAsync()
        {
            //if there are already roles in the system, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            //otherwise we want to create a few roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //I need to use the Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

        }

        private async Task SeedUserAsync()
        {
            //If there are already users in the system
            if(_dbContext.Users.Any())
            {
                return;
            }

            //Creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "mark.zilkovskyi@gmail.com",
                UserName = "mark.zilkovskyi@gmail.com",
                FirstName = "Mark",
                LastName = "Zilkovskyi",
                PhoneNumber = "215-688-8404",
                DisplayName = "Marko",
                EmailConfirmed = true,
            };

            //Use the userManager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abcj@123?");

            //Add this new user to the administrators role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            //Create the moderator user
            var modUser = new BlogUser()
            {
                Email = "fil.zilkovskyi@gmail.com",
                UserName = "fil.zilkovskyi@gmail.com",
                FirstName = "Fil",
                LastName = "Zilkovskyi",
                PhoneNumber = "215-688-8404",
                DisplayName = "Marko",
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(modUser, "Abcj@123?");

            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }


       
    }
}
