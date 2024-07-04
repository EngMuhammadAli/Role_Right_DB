using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication14.Context;
using WebApplication14.Models;
using WebApplication14.Models.RoleModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication14.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public PermissionsController(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult Index(string selectedRole)
        {
            var roles = _roleManager.Roles.ToList();
            var forms = _context.FormAccesses.ToList();
            List<Permission> permissions;

            if (!string.IsNullOrEmpty(selectedRole))
            {
                permissions = _context.Permissions
                    .Where(p => p.RoleName == selectedRole)
                    .ToList();

                // Add default permissions for forms that don't have permissions set for the selected role
                foreach (var form in forms)
                {
                    var existingPermission = permissions.FirstOrDefault(p => p.FormName == form.FormName);
                    if (existingPermission == null)
                    {
                        permissions.Add(new Permission
                        {
                            FormName = form.FormName,
                            RoleName = selectedRole,
                            CanCreate = false,
                            CanRead = false,
                            CanUpdate = false,
                            CanDelete = false
                        });
                    }
                }
            }
            else
            {
                permissions = forms.Select(form => new Permission
                {
                    FormName = form.FormName,
                    RoleName = string.Empty, // Initialize with empty role name
                    CanCreate = false,
                    CanRead = false,
                    CanUpdate = false,
                    CanDelete = false
                }).ToList();
            }

            var model = new GetPermissionsViewModel
            {
                Roles = roles,
                Forms = forms,
                SelectedRole = selectedRole,
                Permissions = permissions
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult SetPermissions(GetPermissionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Handle invalid ModelState here
                return BadRequest(ModelState);
            }

            try
            {
                var permissionsForRole = _context.Permissions
                    .Where(p => p.RoleName == model.SelectedRole)
                    .ToList();

                foreach (var permission in model.Permissions)
                {
                    var existingPermission = permissionsForRole.FirstOrDefault(p => p.FormName == permission.FormName);
                    if (existingPermission == null)
                    {
                        permission.RoleName = model.SelectedRole;
                        _context.Permissions.Add(permission);
                    }
                    else
                    {
                        existingPermission.CanCreate = permission.CanCreate;
                        existingPermission.CanRead = permission.CanRead;
                        existingPermission.CanUpdate = permission.CanUpdate;
                        existingPermission.CanDelete = permission.CanDelete;
                        _context.Permissions.Update(existingPermission);
                    }
                }

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and handle it appropriately
                // You might want to return a specific view with an error message here
                return RedirectToAction("Error", "Home"); // Example of redirecting to an error page
            }

            return RedirectToAction("Index");
        }
    }
}
