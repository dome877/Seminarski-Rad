using Microsoft.AspNetCore.Identity;

namespace Seminarski_Rad.Data
{
    public class ApplicationUserDbInit
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "test@test.hr",
                Email = "test@test.hr"
            };

            var result = userManager.CreateAsync(user, "password").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }

    }
}
