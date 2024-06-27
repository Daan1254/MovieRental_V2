using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRental_V2.Server.Models;
using System.Threading.Tasks;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            if (_signInManager.IsSignedIn(User))
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                return Ok(new
                {
                    IsAuthenticated = true,
                    UserName = user.UserName
                });
            }
            return Ok(new
            {
                IsAuthenticated = false,
                UserName = string.Empty
            });
        }
        
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRegisterModel data)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(data.Email);
            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, data.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new LoginRegisterResultModel { Successful = true });
                }
            }
            return Ok(new LoginRegisterResultModel { Successful = false });
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRegisterModel data)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = data.Email,
                Email = data.Email
            };
            
            IdentityResult result = await _userManager.CreateAsync(user, data.Password);
            
            if (result.Succeeded)
            {
                return Ok(new LoginRegisterResultModel { Successful = true });
            }
            return Ok(new LoginRegisterResultModel { Successful = false });
        }
    }
}