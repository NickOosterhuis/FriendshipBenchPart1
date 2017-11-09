﻿using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _options;

        public AccountController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IOptions<JWTSettings> optionsAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
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
                    return new JsonResult(new Dictionary<string, object>
          {
            { "access_token", GetAccessToken(Credentials.Email) },
            { "id_token", GetIdToken(client) }
          });
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

                    JsonResult token = new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", GetAccessToken(Credentials.Email) },
                        { "id_token", GetIdToken(healthWorker) }
                    });

                    HttpContext.Response.Cookies.Append("token", token.ContentType);

                    return token;
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
                    return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", GetAccessToken(Credentials.Email) },
                        { "id_token", GetIdToken(user) },
                    });
                }

                return new JsonResult("Unable to sign in") { StatusCode = 401 };
            }

            return Error("Unexpected error");
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
            var userId = user.Id; 

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private string GetIdToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { "id", user.Id },
                { "sub", user.Email },
                { "email", user.Email },
                { "emailConfirmed", user.EmailConfirmed },
            };
            return GetToken(payload);
        }

        private string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>
            {
                { "sub", Email },
                { "email", Email }
            };
                return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aud", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
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
