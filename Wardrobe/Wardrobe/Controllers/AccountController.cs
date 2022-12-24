using Entity.Identity;
using Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wardrobe.Model;

namespace Wardrobe.Controllers
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
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login input)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
                var dataModel = await _userManager.FindByEmailAsync(input.Username);
                if (!loginResult.Succeeded)
                {
                    return BadRequest("Kullanıcı adı ya da şifre hatalı");
                }


                //if user is locked
                if (loginResult.IsLockedOut)
                {

                    var localTime = Convert.ToDateTime(dataModel.LockoutEnd.ToString()).ToLocalTime();

                    return BadRequest($"User blocked. Please wait until {localTime}.");
                }

                //if wrong password, increase failed attempt count
                if (!loginResult.Succeeded)
                {
                    await _userManager.AccessFailedAsync(dataModel);

                    return BadRequest("Email or password is wrong.");
                }

                var user = await _userManager.FindByNameAsync(input.Username);
                return Ok(GetTokenResponse(user));
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register input)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser
                {
                    UserName = input.Email,
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    NationalIdNumber = input.NationalIdNumber
                };

                var registerUser = await _userManager.CreateAsync(newUser, input.Password);
                if (registerUser.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    var user = await _userManager.FindByNameAsync(newUser.UserName);
                    return Ok("Kayıt Başarılı. Hoş geldiniz");
                }
                AddErrors(registerUser);
            }
            return BadRequest(ModelState);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("error", err.Description);
            }
        }
        private JwtTokenResult GetTokenResponse(ApplicationUser user)
        {
            var token = GetToken(user);
            JwtTokenResult result = new JwtTokenResult
            {
                AccessToken = token,
                ExpireInSeconds = _configuration.GetValue<int>("Tokens:Lifetime"),
                UserId = user.Id
            };
            return result;
        }

        private string GetToken(ApplicationUser user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("Tokens:Lifetime")),
                audience: _configuration.GetValue<string>("Tokens:Audience"),
                issuer: _configuration.GetValue<string>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


    }
}
