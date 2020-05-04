using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public static class DataInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "User"
                };

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin"
                };

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Counselor").Result)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Counselor"
                };

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("Admin@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    
                    Email = "Admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin@gmail.com",
                    DOB = DateTime.Now ,
                    Gender = "male",
                    Country = "Lebanon",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password 123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("Adam@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "Adam@gmail.com",
                    FirstName = "Adam",
                    LastName = "Smith",
                    Gender = "male",
                    Country = "Lebanon",
                    UserName = "Adam@gmail.com",
                    DOB = DateTime.Now,
                    EmailConfirmed = true
                };

                IdentityResult result =  userManager.CreateAsync(user, "Password 123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("nancy@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "nancy@gmail.com",
                    FirstName = "Nancy",
                    LastName = "Smith",
                    Country = "Lebanon",
                    Gender = "female",
                    UserName = "nancy@gmail.com",
                    DOB = DateTime.Now,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password 123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("hasankenaan@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "hasankenaan@gmail.com",
                    FirstName = "hasan",
                    LastName = "kenaan",
                    Country = "Lebanon",
                    Gender = "male",
                    UserName = "hasankenaan@gmail.com",
                    DOB = DateTime.Now,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password 123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Counselor").Wait();
                }
            }

            if (userManager.FindByEmailAsync("venus@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "venus@gmail.com",
                    FirstName = "venus",
                    LastName = "marwani",
                    Country = "Lebanon",
                    Gender = "female",
                     UserName = "venus@gmail.com",
                    DOB = DateTime.Now,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password 123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Counselor").Wait();
                }
            }

        }

        



        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
    }
}
