using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Context;
using WebApplication14.Models.RoleModel;
using WebApplication14.Models.UserModel;

namespace WebApplication14.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {

            _roleManager = roleManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public IActionResult Index() => View(_roleManager.Roles.ToList());
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var result = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(roleName);
        }
    }
}
