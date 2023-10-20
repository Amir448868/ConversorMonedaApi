using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedaApi.Controllers
{
   
        [Route("api/currency")]
        [ApiController]
        public class CurrencyController : ControllerBase
        {
            private readonly ConversorContext _context;

            public CurrencyController(ConversorContext context)
            {
                _context = context;
            }

            // Endpoint para obtener todas las monedas
            [HttpGet]
            public ActionResult<IEnumerable<Currency>> Get()
            {
                var currencies = _context.Currencies.ToList();
                return Ok(currencies);
            }

            // Endpoint para obtener una moneda por ID
            [HttpGet("{id}")]
            public ActionResult<Currency> Get(int id)
            {
                var currency = _context.Currencies.Find(id);
                if (currency == null)
                {
                    return NotFound();
                }
                return Ok(currency);
            }

            // Endpoint para crear una nueva moneda
            [HttpPost]
            public ActionResult<Currency> Post([FromBody] CurrencyForCreate currencyTocreate)
            {
                var currency = new Currency
                {
                    Name = currencyTocreate.Name,
                    Symbol = currencyTocreate.Symbol,
                    Value = currencyTocreate.Value
                };
                _context.Currencies.Add(currency);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = currency.MonedaId }, currency);
            }

            // Endpoint para actualizar una moneda por ID
            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] CurrencyForCreate updatedCurrency)
            {
                var currency = _context.Currencies.Find(id);
                if (currency == null)
                {
                    return NotFound();
                }

                currency.Value = updatedCurrency.Value;

                _context.SaveChanges();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var currency = _context.Currencies.Find(id);
                if (currency == null)
                {
                    return NotFound();
                }

                _context.Currencies.Remove(currency);
                _context.SaveChanges();

                return NoContent();
            }
        }

    }

