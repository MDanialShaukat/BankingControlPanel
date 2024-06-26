using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BankingControlPanel.Dtos;
using BankingControlPanel.Helpers;
using BankingControlPanel.Models;

namespace BankingControlPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Registration of a user
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role!);
                return Ok(new { result = "User created successfully" });
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login the user and gives the authentication token for next API calls
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByEmailAsync(model.Email!);
                var roles = await _userManager.GetRolesAsync(appUser);
                var token = JwtTokenHelper.GenerateJwtToken(appUser, _configuration, roles);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
