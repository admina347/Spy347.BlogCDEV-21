using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private IMapper _mapper;

        public RoleService(IMapper mapper, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Guid> CreateRole(RoleViewModel model)
        {
            var role = new Role() { Name = model.Name, Description = model.Description, SecurityLevel = model.SecurityLevel };
            await _roleManager.CreateAsync(role);

            return Guid.Parse(role.Id);
        }

        public async Task<RoleViewModel> EditRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            var model = new RoleViewModel()
            {
                Id = id,
                Name = role.Name,
                Description = role.Description,
                SecurityLevel = (int)role.SecurityLevel
            };

            return model;
        }


        public async Task EditRole(RoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) && model.SecurityLevel == null)
                return;

            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            if (!string.IsNullOrEmpty(model.Name))
                role.Name = model.Name;
            if (!string.IsNullOrEmpty(model.Description))
                role.Description = model.Description;
            if (model.SecurityLevel != null)
                role.SecurityLevel = model.SecurityLevel;

            await _roleManager.UpdateAsync(role);
        }

        public async Task RemoveRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}