namespace WebApplication14.Services
{
    public interface IPermissionService
    {

        Task<bool>  CanCreate(string formName);
        Task<bool> CanRead(string formName);
        Task<bool> CanUpdate(string formName);
        Task<bool> CanDelete(string formName);
    }
}
