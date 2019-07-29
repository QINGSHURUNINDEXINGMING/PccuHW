using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Test.Models;

[assembly: OwinStartupAttribute(typeof(Test.Startup))]
namespace Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("SuperAdmin"))
            {

                var user = new ApplicationUser();
                user.UserName = "A5254135@g.pccu.edu.tw";
                user.Email = "A5254135@g.pccu.edu.tw";
                string pwd = "A32254748a@";

                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "SuperAdmin");
                }
                var role = new IdentityRole("SuperAdmin");
                roleManager.Create(role);
            }

        }
    }
}
