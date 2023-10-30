using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedaApi.Controllers
{
   
        [Route("api/currency")]
        [ApiController]
        public class CurrencyController : ControllerBase
        {

            private readonly CurrencyServices _currencyServices;

            public CurrencyController(CurrencyServices currencyServices )
            {
                _currencyServices = currencyServices;
            }

            // Endpoint para obtener todas las monedas
            [HttpGet]
            public ActionResult<Currency> Get()
            {
       
                return Ok(_currencyServices.GetAll());
            }

            // Endpoint para obtener una moneda por ID
            [HttpGet("{id}")]
            public ActionResult<Currency> Get(int id)
            {
            var currency = _currencyServices.GetById(id);
            if (currency == null)
                {
                    return NotFound();
                }
                return Ok(currency);
            }

            // Endpoint para crear una nueva moneda
            [HttpPost]
            public ActionResult<Currency> Post([FromBody] CurrencyForCreate currencyToCreate)
            {
            var currency = _currencyServices.CreateCurrency(currencyToCreate);

            // Retorna un objeto ActionResult con CreatedAtAction para indicar la creación exitosa.
            return CreatedAtAction(nameof(Get), new { id = currency.MonedaId }, currency);
            }

            // Endpoint para actualizar una moneda por ID
            [HttpPut("{id}")]
            public IActionResult Put(int id, [FromBody] CurrencyForCreate updatedCurrency)
            {
            var updatedCurrencyEntity = _currencyServices.UpdateCurrency(id, updatedCurrency);

            if (updatedCurrencyEntity == null)
            {
                return NotFound();
            }

            return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
            if (!_currencyServices.DeleteCurrency(id))
            {
                return NotFound();
            }

            return NoContent();
            }
        }

    }

