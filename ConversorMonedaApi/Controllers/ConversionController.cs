using ConversorMonedaApi.Data;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;
using ConversorMonedaApi.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConversionController : ControllerBase
    {
        private readonly ConversionServices _conversionServices;
        public ConversionController(ConversionServices conversionServices)
        {
            _conversionServices = conversionServices;
        }


        [HttpPost]
        public IActionResult Post([FromBody] ConversionForCreate conversionToCreate )
        {
            try
            {
                if (!_conversionServices.DeductRemainingRequest(conversionToCreate.UserId))
                {
                    return BadRequest(new { mensaje = "Has alcanzado el límite de solicitudes." });
                }

                var conversion = _conversionServices.CreateConversion(conversionToCreate);

                if (conversion == null)
                {
                    return BadRequest(new { mensaje = "La moneda no existe." });
                }

                return Ok(conversion);
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al crear la conversión." });
            }
        }


        [HttpGet("{Userid}")]
        public IActionResult Get(int userid)
        {
                return Ok(_conversionServices.GetConversionById(userid));   
        }

        [HttpGet("GetRemainingRequest/{userId}")]
        public IActionResult GetRemainingRequest(int userId)
        {
            return Ok(_conversionServices.GetRemainingRequest(userId));
        }
       
    }
}
