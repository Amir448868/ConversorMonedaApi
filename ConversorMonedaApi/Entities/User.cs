using ConversorMonedaApi.Data.Models.Enum;
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

        public int ConversionCounter { get; set; }

        public Role Role { get; set; } = Role.User;

        public List<Conversion>? Conversions { get; set; }

    }
}
