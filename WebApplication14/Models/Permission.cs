namespace WebApplication14.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string FormName { get; set; }
        public string RoleName { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
