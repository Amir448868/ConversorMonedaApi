using ConversorMonedaApi.Data;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Text.Json;
using ConversorMonedaApi.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController: ControllerBase
    {
        private readonly ConversionServices _conversionServices;
        public ConversionController( ConversionServices conversionServices)
        {
            _conversionServices = conversionServices;
        }




        [HttpPost]

        
        public IActionResult Post([FromQuery] ConversionForCreate conversionToCreate )
        {
            var conversion = _conversionServices.CreateConversion(conversionToCreate);

            if (conversion == null)
            {
                return BadRequest("La moneda no existe.");
            }
          
            if (!_conversionServices.DeductRemainingRequest(conversionToCreate.UserId))
            {
               return BadRequest("Has alcanzado el límite de solicitudes.");
            }

            
            return Ok(conversion);
        }

        [HttpGet]
   
        public IActionResult GetAllConversions()
        {
            return Ok(_conversionServices.GetConversions());
        }

        [HttpGet("{Userid}")]
        public IActionResult Get(int Userid)
        {
            return Ok(_conversionServices.GetConversionById(Userid));
        }
       
    }
}
