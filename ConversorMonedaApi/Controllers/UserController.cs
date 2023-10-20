using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ConversorContext _context;
        public UserController(ConversorContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post([FromQuery] UserForCreation userTocreate )
        {
            var user = new User
            {
                UserName = userTocreate.UserName,
                Password = userTocreate.Password,
                TypeUser = userTocreate.TypeUser,
                RemainingRequests = TypeUserHelper.GetRole(userTocreate.TypeUser)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
/*
 Este controlador se encargaría de las operaciones relacionadas con la gestión de usuarios, como el registro, inicio de sesión,
recuperación de contraseñas y cualquier operación relacionada con la autenticación y gestión de cuentas de usuario.
 */