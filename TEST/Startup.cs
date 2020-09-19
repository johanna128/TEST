using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TEST.Models;

[assembly: OwinStartupAttribute(typeof(TEST.Startup))]
namespace TEST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                // Create Admin role    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //create Admin super user who will maintain the website              
                var user = new ApplicationUser();
                user.UserName = "test2@test.com";
                user.Email = "test2@test.com";

                string userPWD = "Test1!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Admin Role     
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // Creat Operator role     
            if (!roleManager.RoleExists("Operator"))
            {
                var role = new IdentityRole();
                role.Name = "Operator";
                roleManager.Create(role);

            }
        }
    }
}
