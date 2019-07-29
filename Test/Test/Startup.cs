using Microsoft.Owin;
using Owin;
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

            if (!roleManager.RoleExists("Admin12345"))
            {

                var role = new IdentityRole("Admin12345");
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "sai@do.com";
                user.Email = "sai@do.com";
                string pwd = "A32254748a@";

                var newuser = userManager.Create(user, pwd);

                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin12345");

                }
                //var store = new UserStore<ApplicationUser>(context);

                //var manager = new ApplicationUserManager(store);
                //var user = new ApplicationUser()
                //{
                //    Email = "adm123@qq.com",
                //    UserName = "adm123@qq.com"
                //};
                //manager.Create(user, "Adm@123");
                //manager.AddToRole(user.Id, "Admin12345");
            }

        }
    }
}
