using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public UserController(IUserData userData )
        {
            _userData = userData;
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
