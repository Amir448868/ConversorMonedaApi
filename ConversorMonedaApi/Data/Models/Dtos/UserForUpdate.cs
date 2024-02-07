using ConversorMonedaApi.Data.Models.Enum;

namespace ConversorMonedaApi.Data.Models.Dtos
{
    public class UserForUpdate
    {
        public string? UserName { get; set; }
        public string TypeUser { get; set; } = "free";

        public int Roleid { get; set; } = (int)Role.User;

    }
}
