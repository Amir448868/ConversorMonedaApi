using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConversorMonedaApi.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConversionId { get; set; }

        // Cambiar el tipo de las propiedades relacionadas con las monedas a objetos Currency
        public int CurrencyFromId { get; set; } // moneda de origen
        public int CurrencyToId { get; set; } // moneda de destino

        public int Amount { get; set; }
        public double Result { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }

        // Propiedades de navegación para las monedas de origen y destino
        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        public User User { get; set; }
    }
}
