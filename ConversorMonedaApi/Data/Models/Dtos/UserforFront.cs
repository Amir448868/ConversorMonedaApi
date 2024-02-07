namespace ConversorMonedaApi.Data.Models.Dtos
{
    public class UserforFront
    {
        public int userId { get; set; }
        public string? UserName { get; set; }
        public string? TypeUser { get; set; }

        public int ConversionCounter { get; set; }

        public int Roleid { get; set; }


    }
}
