using ConversorMonedaApi.Data;
using ConversorMonedaApi.Entities;
using SQLitePCL;

namespace ConversorMonedaApi.Services
{
    public class ConversionServices
    {

        public static double Convert(double amount, Currency fromCurrency, Currency toCurrency)
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