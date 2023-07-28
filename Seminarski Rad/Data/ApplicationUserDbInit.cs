using Microsoft.AspNetCore.Identity;

namespace Seminarski_Rad.Data
{
    public class ApplicationUserDbInit
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "domagoj@seminarski.hr",
                Email = "domagoj@seminarski.hr"
            };

            // kreiranje korisnika s passwordom
            var result = userManager.CreateAsync(user, "Password12345").Result;

            // ako je sve ok
            if (result.Succeeded)
            {
                //dodajem korisniku ulogu admnistratora
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
