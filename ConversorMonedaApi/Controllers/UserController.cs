using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
      
        private readonly UserServices _userServices;
        public UserController( UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public IActionResult Post([FromQuery] UserForCreation userTocreate )
        {
            var user = _userServices.CreateUser(userTocreate);
            return Ok(user);
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userServices.GetUsers());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
