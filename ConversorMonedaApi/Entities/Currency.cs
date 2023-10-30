using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversorMonedaApi.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int MonedaId { get; set; }

        public string? Name { get; set; }

        public string? Symbol { get; set; }

        public double Value { get; set; }

    }
}
