using ConversorMonedaApi.Data;
using ConversorMonedaApi.Data.Models.Dtos;
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


        public List<ConversionWithCurrencyNames> GetConversionById(int userId)
        {
            var conversionsWithCurrencyNames = (
                from conversion in _context.Conversions
                join currencyFrom in _context.Currencies on conversion.CurrencyFromId equals currencyFrom.MonedaId
                join currencyTo in _context.Currencies on conversion.CurrencyToId equals currencyTo.MonedaId
                where conversion.UserId == userId
                select new ConversionWithCurrencyNames
                {
                    ConversionId = conversion.ConversionId,
                    CurrencyFromName = currencyFrom.Symbol,
                    CurrencyToName = currencyTo.Symbol,
                    Amount = conversion.Amount,
                    Result = conversion.Result,
                    Date = conversion.Date,
                }
            ).ToList();

            return conversionsWithCurrencyNames;
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

        public int GetRemainingRequest(int userId)
        {
            
            var user = _context.Users.Find(userId);

            if (user == null)
            {
               
                return 0;
            }

            
            var subscription = _context.Subscriptions.FirstOrDefault(r => r.TypeUser == user.TypeUser);

            if (subscription == null)
            {
                return 0;
            }

            if (subscription.Value < 0)
            {
                return 0;
            }

            int remainingRequests = subscription.Value - user.ConversionCounter;

            return Math.Max(0, remainingRequests);
        }
        public bool DeductRemainingRequest(int userId)
        {
            var user = _context.Users.Find(userId);
            var remainingRequest = _context.Subscriptions.FirstOrDefault(r => r.TypeUser == user.TypeUser);

            if (user == null)
            {
                return false; // Puedes manejar el error adecuadamente, por ejemplo, lanzando una excepción personalizada.
            }

            if (user.ConversionCounter == remainingRequest.Value)
            {
                return false;
            }
            user.ConversionCounter++;
            _context.SaveChanges();
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

