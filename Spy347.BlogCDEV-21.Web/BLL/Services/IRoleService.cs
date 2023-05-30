using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface IRoleService
    {
        Task<Guid> CreateRole(RoleViewModel model);
        Task EditRole(RoleViewModel model);
        Task<List<Role>> GetRoles();
        Task RemoveRole(Guid id);
    }
}