using Microsoft.AspNetCore.Identity;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels.Account;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface IAccountService
    {
        Task<UserEditViewModel> EditAccount(int id);
        Task<IdentityResult> EditAccount(UserEditViewModel model);
        Task<List<User>> GetAccounts();
        Task<SignInResult> Login(LoginViewModel model);
        Task LogoutAccount();
        Task<IdentityResult> Register(RegisterViewModel model);
        Task RemoveAccount(int id);
    }
}