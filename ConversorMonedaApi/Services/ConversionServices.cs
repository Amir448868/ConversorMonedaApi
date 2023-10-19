using ConversorMonedaApi.Data;
using ConversorMonedaApi.Entities;
using SQLitePCL;

namespace ConversorMonedaApi.Services
{
    public class ConversionServices
    {
        
        public static int Convert(string FromCurrency, string ToCurrency, int Amount)
        {
            if (FromCurrency == "ARS")
            {
                if (ToCurrency == "ARS")
                {
                    return Amount;
                }
                else if (ToCurrency == "USD")
                {
                    return Amount / 95;
                }
                else if (ToCurrency == "EUR")
                {
                    return Amount / 115;
                }
                else if (ToCurrency == "GBP")
                {
                    return Amount / 135;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

       
    }
}
/*Este servicio sería responsable de realizar las conversiones de moneda.
 * Debería incluir métodos para llevar a cabo las conversiones, 
 * registrarlas en la base de datos y verificar los límites de solicitudes por usuario. 
 * Además, puede ser útil para implementar lógica adicional, como la obtención de tasas de cambio actualizadas.*/