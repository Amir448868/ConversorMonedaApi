using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;

using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Text.Json;

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
            var currencyFrom = _context.Currencies.SingleOrDefault(c => c.Symbol == conversionTocreate.FromCurrency);
            var currencyTo = _context.Currencies.SingleOrDefault(c => c.Symbol == conversionTocreate.ToCurrency);

            if (currencyFrom == null || currencyTo == null)
            {
                return BadRequest("Las monedas especificadas no existen en la base de datos.");
            }

            var conversion = new Conversion
            {
                CurrencyFromId = currencyFrom.MonedaId,
                CurrencyToId = currencyTo.MonedaId,
                Amount = conversionTocreate.Amount,
                Result = ConversionServices.Convert(conversionTocreate.Amount,currencyFrom,currencyTo),
                UserId = conversionTocreate.UserId,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };


            int remainingRequests = _context.Users.Find(conversionTocreate.UserId).RemainingRequests;



            if (remainingRequests == 0)
            {
                return BadRequest("Has alcanzado el límite de solicitudes.");
            }

            remainingRequests--;
            _context.Users.Find(conversionTocreate.UserId).RemainingRequests = remainingRequests;





            _context.Conversions.Add(conversion);
            _context.SaveChanges();
            return Ok(conversion.Result);
        }


     

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Conversions.ToList());
        }

        [HttpGet("{Userid}")]
        public IActionResult Get(int Userid)
        {
            return Ok(_context.Conversions.Where(c => c.UserId == Userid).ToList());
        }
       
    }
}
