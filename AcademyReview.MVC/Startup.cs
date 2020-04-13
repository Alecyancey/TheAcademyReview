using AcademyReview.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcademyReview.MVC.Startup))]
namespace AcademyReview.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SeedDefaultRolesAndUsers();
        }
        private void SeedDefaultRolesAndUsers()
        {
            // create dbContext
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            // if Admin role doesn't exist,  create an admin role, 

            if (!roleManager.RoleExists("Admin"))
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "Admin"
                };

                roleManager.Create(identityRole);


                // create our first user,
                var user = new ApplicationUser() { FirstName = "UserFirstName", LastName = "UserLastName", UserName = "admin@email.com", Email = "admin@email.com" };
                var password = "Password1!";
                userManager.Create(user, password);

                //var userId = User.Identity.GetUserId(user);

                //then add user to admin role

                userManager.AddToRole(user.Id, identityRole.Name);
            }

            // if role:"user" doesn't exist, create a "user" role
            if (!roleManager.RoleExists("User"))
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = "User"
                };

                roleManager.Create(identityRole);
            }
        }
    }
}
