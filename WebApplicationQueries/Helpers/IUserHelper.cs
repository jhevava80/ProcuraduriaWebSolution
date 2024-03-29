﻿using Microsoft.AspNetCore.Identity;
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

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldpassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync (User user, string roleName);

        Task<bool> IsUserInRoleAsync (User user, string roleName);

    }
}
