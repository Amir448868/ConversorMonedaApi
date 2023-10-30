using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConversorMonedaApi.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? TypeUser { get; set; }

        public int RemainingRequests { get; set; }

        public List<Conversion>? Conversions { get; set; }

    }
}
