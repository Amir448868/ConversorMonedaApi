﻿namespace ConversorMonedaApi.Data.Models.Dtos
{
    public class UserForCreation
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string TypeUser { get; set; } = "free";

    }
}