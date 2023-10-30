using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models;
using ConversorMonedaApi.Entities;
using SQLitePCL;

namespace ConversorMonedaApi.Services
{
    public class ConversionServices
    {
        private readonly ConversorContext _context;
        public ConversionServices(ConversorContext context)
        {
            _context = context;
        }

        public List<Conversion> GetConversions()
        {
            return _context.Conversions.ToList();

        }

        public List<Conversion> GetConversionById(int Userid)
        {
            return _context.Conversions.Where(c => c.UserId == Userid).ToList();
        }

        public Conversion CreateConversion(ConversionForCreate conversionToCreate)
        {
            var currencyFrom = _context.Currencies.SingleOrDefault(c => c.Symbol == conversionToCreate.FromCurrency);
            var currencyTo = _context.Currencies.SingleOrDefault(c => c.Symbol == conversionToCreate.ToCurrency);

            if (currencyFrom == null || currencyTo == null)
            {
                return null; // Puedes manejar el error adecuadamente, por ejemplo, lanzando una excepción personalizada.
            }

            var conversion = new Conversion
            {
                CurrencyFromId = currencyFrom.MonedaId,
                CurrencyToId = currencyTo.MonedaId,
                Amount = conversionToCreate.Amount,
                Result = Convert(conversionToCreate.Amount, currencyFrom, currencyTo),
                UserId = conversionToCreate.UserId,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };

            _context.Conversions.Add(conversion);
            _context.SaveChanges();

            return conversion;

        }

        public bool DeductRemainingRequest(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return false; // Puedes manejar el error adecuadamente, por ejemplo, lanzando una excepción personalizada.
            }

            if (user.RemainingRequests == 0)
            {
                return false;
            }

            user.RemainingRequests--;
            return true;
        }


        private double Convert(double amount, Currency fromCurrency, Currency toCurrency)
        {
            if (fromCurrency != null && toCurrency != null)
            {
                if (fromCurrency.MonedaId == toCurrency.MonedaId)
                {
                    return amount; // Misma moneda, no se requiere conversión.
                }

                if (fromCurrency.Value > 0 && toCurrency.Value > 0)
                {
                    return amount * fromCurrency.Value / toCurrency.Value;
                }
            }

            return 0; // Si no se pueden obtener tasas de cambio válidas, devuelve 0.
        }

    }
}

/*Este servicio sería responsable de realizar las conversiones de moneda.
 * Debería incluir métodos para llevar a cabo las conversiones, 
 * registrarlas en la base de datos y verificar los límites de solicitudes por usuario. 
 * Además, puede ser útil para implementar lógica adicional, como la obtención de tasas de cambio actualizadas.*/