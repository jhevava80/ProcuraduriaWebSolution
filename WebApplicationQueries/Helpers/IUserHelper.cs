using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplicationSearch.Data.Entities;
using WebApplicationSearch.Models;

namespace WebApplicationSearch.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
