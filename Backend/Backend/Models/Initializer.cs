using Backend.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Initializer
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(RoleModel.Admin))
            {
                await roleManager.CreateAsync(new ApplicationRole(RoleModel.Admin));
            }
            if (!await roleManager.RoleExistsAsync(RoleModel.User))
            {
                await roleManager.CreateAsync(new ApplicationRole(RoleModel.User));
            }

            if (await userManager.FindByEmailAsync("ministoir@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "ministoir@gmail.com",
                    Email = "ministoir@gmail.com"
                };
                var password = "P_assword1";
                var result = await userManager.CreateAsync(user, password);
            
                if(!result.Succeeded)
                {
                    throw new Exception("Failed to create local user");
                }
                await userManager.AddToRolesAsync(user, new List<string> { RoleModel.Admin, RoleModel.User });
            }
        }
    }
}