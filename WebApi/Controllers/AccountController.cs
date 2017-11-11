using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ViewModels;
using WebApi.ViewModels.Account;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _options;
        private readonly IConfiguration _config; 

        public AccountController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IOptions<JWTSettings> optionsAccessor,
          IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminViewModel Credentials)
        {
            if (ModelState.IsValid)
            {
                var admin = new User
                {
                    UserName = Credentials.Email,
                    Email = Credentials.Email,
                };
                var result = await _userManager.CreateAsync(admin, Credentials.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                    await _signInManager.SignInAsync(admin, isPersistent: false);
                    return Ok("User successfully registered");
                }
                return Errors(result);
            }
            return Error("Unexpected error");
        }

        //POST /api/account/register/client
        [AllowAnonymous]
        [HttpPost("register/client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientViewModel Credentials)
        {
            if (ModelState.IsValid)
            {
                var client = new ClientUser {
                    UserName = Credentials.Email,
                    Email = Credentials.Email,
                    Firstname = Credentials.FirstName,
                    Lastname = Credentials.LastName,
                    Gender = Credentials.Gender,
                    Birthday = Credentials.BirthDay, 
                    StreetName = Credentials.StreetName,
                    HouseNumber = Credentials.HouseNumber,
                    Province = Credentials.Province,
                    District = Credentials.District,
                };
                var result = await _userManager.CreateAsync(client, Credentials.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(client, "client"); 
                    await _signInManager.SignInAsync(client, isPersistent: false);
                    return Ok("User successfully registered");
                }
                return Errors(result);

            }
            return Error("Unexpected error");
        }

        //POST /api/account/register/healthworker
        [Authorize(Roles = "admin")]
        [HttpPost("register/healthworker")]
        public async Task<IActionResult> RegisterHealthWorker([FromBody] RegisterHealthWorkerViewModel Credentials)
        {
            if (!User.IsInRole("admin"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var healthWorker = new HealthWorkerUser
                {
                    UserName = Credentials.Email,
                    Email = Credentials.Email,
                    Firstname = Credentials.FirstName,
                    Lastname = Credentials.LastName,
                    Gender = Credentials.Gender,
                    Birthday = Credentials.BirthDay,
                    PhoneNumber = Credentials.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(healthWorker, Credentials.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(healthWorker, "healthworker");
                    await _signInManager.SignInAsync(healthWorker, isPersistent: false);
                    return Ok("User successfully registered");
                }
                return Errors(result);

            }
            return Error("Unexpected error");
        }

        //POST /api/account/signin
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel Credentials)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Credentials.Email, Credentials.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Credentials.Email);

                    return Ok("User signed in successfull");
                }

                return new JsonResult("Unable to sign in") { StatusCode = 401 };
            }

            return Error("Unexpected error");
        }

        [AllowAnonymous]
        [HttpPost("generatetoken")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:SecretKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _config["JWTSettings:Issuer"],
                            audience: _config["JWTSettings:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                    }
                }
            }

            return BadRequest("Could not create token");
        }

        //POST /api/account/signout
        [Authorize]
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return new JsonResult("User is Logged out");
        }

        //GET api/account/user
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User); 

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private JsonResult Errors(IdentityResult result)
        {
            var items = result.Errors
                .Select(x => x.Description)
                .ToArray();
            return new JsonResult(items) { StatusCode = 400 };
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
