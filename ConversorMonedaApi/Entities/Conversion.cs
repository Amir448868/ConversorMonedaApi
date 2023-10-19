using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConversorMonedaApi.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConversionId { get; set; }
        public string FromCurrency { get; set; } //moneda de origen
        public string ToCurrency { get; set; } //moneda de destino
        public int Amount { get; set; }
        public double Result { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
