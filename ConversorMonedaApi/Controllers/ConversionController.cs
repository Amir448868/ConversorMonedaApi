using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConversorMonedaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController: ControllerBase
    {
        private readonly ConversorContext _context;
        public ConversionController(ConversorContext context)
        {
            _context = context;
        }

        
       

        [HttpPost]

        
        public IActionResult Post([FromQuery] ConversionForCreate conversionTocreate )
        {
            var conversion = new Conversion
            {
                FromCurrency = conversionTocreate.FromCurrency,
                ToCurrency = conversionTocreate.ToCurrency,
                Amount = conversionTocreate.Amount,
                Result = ConversionServices.Convert(conversionTocreate.FromCurrency, conversionTocreate.ToCurrency, conversionTocreate.Amount),
                UserId = conversionTocreate.UserId,
                Date= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")

            };

            int requestCount = _context.RequestsLog.Count(r => r.UserId == conversionTocreate.UserId);

            if (requestCount >= 1)
            {
                return BadRequest("Has alcanzado el límite de 20 solicitudes.");
            }

            var requestLog = new ResquestLog
            {
                UserId = conversionTocreate.UserId,
                
            };

            _context.Conversions.Add(conversion);
            _context.RequestsLog.Add(requestLog);
            _context.SaveChanges();
            return Ok(conversion);
        }


     

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Conversions.ToList());
        }
    }
}
