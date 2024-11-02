using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PHDataManager.Library.DataAccess;
using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;
using System.Security.Claims;

namespace PHApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserData _userData;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserData userData,
                              UserManager<IdentityUser> userManager)
        {
            _userData = userData;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public UserModel GetById()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrWhiteSpace(userId))
            {
                throw new Exception("Cannot get Authorized User");
            }

            return _userData.GetUserById(userId).First();
        }

        public record UserRegistrationModel(string FirstName,
                                            string LastName,
                                            string EmailAddress,
                                            string Password);

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel user)
        {
            if (ModelState.IsValid) 
            {
                var existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);

                if (existingUser is null)
                {
                    IdentityUser newUser = new()
                    {
                        Email = user.EmailAddress,
                        EmailConfirmed = true,
                        UserName = user.EmailAddress
                    };

                    IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                    if (result.Succeeded)
                    {
                        existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);

                        if(existingUser is null)
                        {
                            return BadRequest();
                        }

                        UserModel u = new()
                        {
                            Id = existingUser.Id,
                            FirstName = user.FirstName, 
                            LastName = user.LastName, 
                            EmailAddress = user.EmailAddress,
                        };

                        _userData.CreateUser(u);
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public UserModel? GetUserByEmail(string email) 
        {
            var users = _userData.GetUserByEmail(email);
            if (users.Count == 0)
            {
                Response.StatusCode = 404;
                return null;
            }

            return users[0];  
        }

        [HttpGet]
        [Route("CheckUserProjectMembership/{userId}_{projectId}")]
        public bool CheckUserProjectMembership(string userId, int projectId)
        {
            var result = true;

            var roles = _userData.CheckUserProjectMembership(userId, projectId);

            if (roles.Count == 0)
                result = false;

            return result;
        }
    }
}
