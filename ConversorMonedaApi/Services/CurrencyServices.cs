using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;

namespace ConversorMonedaApi.Services
{
    public class CurrencyServices
    {
        private readonly ConversorContext _context;

        public CurrencyServices(ConversorContext context)
        {
            _context = context;
        }

        public List<Currency> GetAll()
        {
            return _context.Currencies.ToList();
        }

        public Currency GetById(int id)
        {
         return _context.Currencies.Find(id);
        }

        public Currency CreateCurrency(CurrencyForCreate currencyToCreate)
        {
            var currency = new Currency
            {
                Name = currencyToCreate.Name,
                Symbol = currencyToCreate.Symbol,
                Value = currencyToCreate.Value
            };

            _context.Currencies.Add(currency);
            _context.SaveChanges();

            return currency;
        }

        public Currency UpdateCurrency(int id, CurrencyForCreate updatedCurrency)
        {
            var currency = _context.Currencies.Find(id);
            if (currency == null)
            {
                return null; // Puedes manejar el error adecuadamente, por ejemplo, lanzando una excepción personalizada.
            }

            currency.Value = updatedCurrency.Value;

            _context.SaveChanges();

            return currency;
        }


        public bool DeleteCurrency(int id)
        {
            var currency = _context.Currencies.Find(id);
            if (currency == null)
            {
                return false; // Puedes manejar el error adecuadamente, por ejemplo, lanzando una excepción personalizada.
            }

            _context.Currencies.Remove(currency);
            _context.SaveChanges();

            return true;
        }
    }
}
