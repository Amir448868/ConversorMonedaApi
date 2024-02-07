using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

//string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            try
            {
                var user = _userServices.CreateUser(userTocreate);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest(new {mensaje = "Error al crear el usuario."});
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Get()
        {
            return Ok(_userServices.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _userServices.DeleteUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al eliminar el usuario." });
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserForUpdate updatedUser)
        {
            try
            {
                var updatedUserEntity = _userServices.UpdateUser(id, updatedUser);


                if (updatedUserEntity == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al actualizar el usuario." });
            }
        }
    }
}
