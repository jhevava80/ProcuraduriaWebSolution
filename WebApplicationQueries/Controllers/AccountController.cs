using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplicationSearch.Data.Entities;
using WebApplicationSearch.Helpers;
using WebApplicationSearch.Models;


namespace WebApplicationSearch.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            this.configuration = configuration;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    //Si tiene direccion de retorno va a ella
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "User or password is incorrect.");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user could not be created.");
                    }

                    await this.userHelper.AddUserToRoleAsync(user,"Customer");

                    //var result2 = await this.userHelper.lo (user, model.Password);
                    //this.ModelState.add(string.Empty, "This user is already registered.");
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "This user is already registered.");
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    var response = await this.userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User has been updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }
            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var response = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (response.Succeeded)
                    {
                        return this.RedirectToAction("Changeuser", "Account");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await this.userHelper.ValidatePasswordAsync(user.Result, model.Password);
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Result.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                this.configuration["Tokens:Issuer"],
                                this.configuration["Tokens:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddDays(1),
                                signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return this.Created(string.Empty, results);
                    }
                }
            }
            return this.BadRequest();
        }

        public IActionResult NotAuthorized()
        {
            return this.View();
        }
    }
}