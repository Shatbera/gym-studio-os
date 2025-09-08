using Microsoft.AspNetCore.Identity;

namespace GymStudioOS.Data
{
    public static class IdentitySeeder
    {
        private static readonly string ADMIN_PASSWORD = "Admin123!";
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (string role in AppRoles.All)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser == null)
            {
                adminUser = new ApplicationUser { UserName =  adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(adminUser, ADMIN_PASSWORD);
                await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
            }
        }
    }
}
