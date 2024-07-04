using Microsoft.AspNetCore.Identity;
using WebApplication14.Context;
using WebApplication14.Models.UserModel;
using WebApplication14.Models;

namespace WebApplication14.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<IList<string>> GetUserRolesAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return await _userManager.GetRolesAsync(user);
        }

        private async Task<Permission> GetPermissionAsync(string formName)
        {
            var userRoles = await GetUserRolesAsync();
            return _context.Permissions.FirstOrDefault(p => p.FormName == formName && userRoles.Contains(p.RoleName));
        }

        public async Task<bool> CanCreate(string formName) => (await GetPermissionAsync(formName))?.CanCreate ?? false;
        public async Task<bool> CanRead(string formName) => (await GetPermissionAsync(formName))?.CanRead ?? false;
        public async Task<bool> CanUpdate(string formName) => (await GetPermissionAsync(formName))?.CanUpdate ?? false;
        public async Task<bool> CanDelete(string formName) => (await GetPermissionAsync(formName))?.CanDelete ?? false;
    }
}
