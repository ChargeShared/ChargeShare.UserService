using System.Net;
using ChargeShare.UserService.DAL.DTOs;
using ChargeShare.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChargeShare.UserService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

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
            //proces user
            _userService.RegisterUser(dataDto);
            //proces adres via messagebus
            //return confimation
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
