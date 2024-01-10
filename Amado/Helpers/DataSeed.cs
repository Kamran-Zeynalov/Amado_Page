using Amado.Entities;
using Microsoft.AspNetCore.Identity;

namespace Amado.Helpers
{
    public class DataSeed
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = new[] { "Admin" };

                foreach (string role in roles)
                {
                    var exists = await roleManager.RoleExistsAsync(role);
                    if (exists) continue;
                    await roleManager.CreateAsync(new IdentityRole(role));
                }


                var user = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@mail.com",
                    UserName = "admin@mail.com",
                };

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var existingUser = await userManager.FindByNameAsync("admin@mail.com");
                if (existingUser is not null) return;

                await userManager.CreateAsync(user, "hello12345");
                await userManager.AddToRoleAsync(user, roles[0]);

                return;
            }
        }
    }
}
