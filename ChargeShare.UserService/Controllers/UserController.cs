using System.Collections;
using System.ComponentModel;
using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChargeShare.UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Used to save Errors for the ModelState since we decouple the ModelState by calling the service this is a workaround to have proper Http response logging
        /// </summary>
        private IEnumerable<IdentityError> _errors  { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hallo", "Falco" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<HttpStatusCode> Post([FromBody] UserRegisterDTO dataDto)
        {
            //Checks if the info is received properly in JSON format
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is good!");
                _errors = await _userService.RegisterUser(dataDto);
            }
            else
            {
                //Adds the errors to the modelstate which will be returned in JSON format on a failed POST
                foreach (var error in _errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            //proces user
            //proces adres via messagebus

            return HttpStatusCode.OK;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
