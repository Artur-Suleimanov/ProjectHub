using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PHApi.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PHApi.Controllers
{
    public class TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            if(await IsValidUserNameAndPassword(username, password))
            {
                return new ObjectResult(await GenerateToken(username));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> IsValidUserNameAndPassword(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if(user == null) await Task.FromResult(false);

            return await _userManager.CheckPasswordAsync(user!, password);
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyIsSecretSoDoNotTellAnINeed256bytePlease")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username,
            };

            return output;
        }
    }
}
