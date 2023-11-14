using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

//string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


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
        public IActionResult Post([FromBody] UserForCreation userTocreate )
        {
            var user = _userServices.CreateUser(userTocreate);
            if (user == null)
            {
                return BadRequest();
            }
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserForUpdate updatedUser)
        {

            var updatedUserEntity = _userServices.UpdateUser(id, updatedUser);
            /*var roles = User.Claims.FirstOrDefault(c => c.Type == "Role").Value;
            if (roles != "Admin") return Unauthorized();*/

            if (updatedUserEntity == null)
            {
                return NotFound();
            }

            return Ok(updatedUserEntity);
        }
    }
}
