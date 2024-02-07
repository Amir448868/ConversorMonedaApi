using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
using ConversorMonedaApi.Entities;
using ConversorMonedaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedaApi.Controllers
{

    [Route("api/currency")]
        [ApiController]
        [Authorize]

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

       

        // Endpoint para crear una nueva moneda

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Currency> Post([FromBody] CurrencyForCreate currencyToCreate)
            {
            try
            {
                var currency = _currencyServices.CreateCurrency(currencyToCreate);
                return Ok(currency);
                //CreatedAtAction(nameof(Get), new { id = currency.MonedaId }, currency);
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al crear la moneda." });
            }


            }

         [HttpPut("{id}")]
         [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] CurrencyForCreate updatedCurrency)
            {
            try
            {
                var updatedCurrencyEntity = _currencyServices.UpdateCurrency(id, updatedCurrency);


                if (updatedCurrencyEntity == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al actualizar la moneda." });
            }
            }
            

            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
            {
            try
            {
                if (!_currencyServices.DeleteCurrency(id))
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch
            {
                return BadRequest(new { mensaje = "Error al eliminar la moneda." });
            }
           
            }
        }

    }

