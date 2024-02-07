﻿namespace ConversorMonedaApi.Data.Models.Dtos
{
    public class ConversionForCreate
    {
        public string? FromCurrency { get; set; } //moneda de origen
        public string? ToCurrency { get; set; } //moneda de destino
        public int Amount { get; set; }
        public int UserId { get; set; } 
    }
}
