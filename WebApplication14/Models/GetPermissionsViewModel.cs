using WebApplication14.Models.RoleModel;

namespace WebApplication14.Models
{
    public class GetPermissionsViewModel
    {
        public List<ApplicationRole> Roles { get; set; }
        public List<FormAccess> Forms { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        public string SelectedRole { get; set; }
    }
}
