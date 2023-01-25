using System.Collections;
using System.ComponentModel;
using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Shared.Models;
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChargeShare.UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ChargeSharedUserModel> _signInManager;
        private readonly UserManager<ChargeSharedUserModel> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Used to save Errors for the ModelState since we decouple the ModelState by calling the service this is a workaround to have proper Http response logging
        /// </summary>
        private IEnumerable<IdentityError> _errors  { get; set; }

        public UserController(IUserService userService, SignInManager<ChargeSharedUserModel> signInManager, UserManager<ChargeSharedUserModel> userManager, IConfiguration configuration)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: api/<UserController>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "Falco" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user/register
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterPost([FromBody] UserRegisterDTO dataDto)
        {
            //Checks if the info is received properly in JSON format
            if (ModelState.IsValid)
            {
                //Process User data
                Console.WriteLine("Model is good!");
                var result = await _userService.RegisterUser(dataDto);

                var loginResult = await _signInManager.PasswordSignInAsync(result, dataDto.Password, true, false);

                if (!loginResult.Succeeded) return BadRequest("Could not login user!");

                //Process adres via messagebus
                AdresModel adres = new AdresModel
                {
                    City = dataDto.City,
                    Country = dataDto.Country,
                    HouseAddition = dataDto.HouseAddition,
                    HouseNumber = dataDto.HouseNumber,
                    PostalCode = dataDto.PostalCode,
                    Province = dataDto.Province,
                    Region = dataDto.Region,
                    Street = dataDto.Street
                };


            }
            else
            {
                //Adds the errors to the modelstate which will be returned in JSON format on a failed POST
                foreach (var error in _errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
           
            


            return Ok("Registerd and logged in!");
        }

        // POST api/user/login
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginPost([FromBody] LoginDTO player)
        {
            if (ModelState.IsValid)
            {

                var result = await _userManager.FindByEmailAsync(player.Email);
                if (result == null)
                    return NotFound();

                var loginResult = await _signInManager.PasswordSignInAsync(result, player.Password, true, false);

                if (!loginResult.Succeeded) return BadRequest("Username and password are invalid.");

                await _userManager.AddClaimAsync(result, new Claim(ClaimTypes.Email, player.Email));

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, player.Email)
                };

                var jwtConfig = _configuration.GetSection("JWT");
                var secretKey = jwtConfig["secret"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(jwtConfig["expiresIn"]));

                var token = new JwtSecurityToken(
                    jwtConfig["validIssuer"],
                    jwtConfig["validAudience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token), Email = player.Email });

            }
            else
            {
                //Adds the errors to the modelstate which will be returned in JSON format on a failed POST
                foreach (var error in _errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return StatusCode(500, "Internal server error");

        }

    }
}
