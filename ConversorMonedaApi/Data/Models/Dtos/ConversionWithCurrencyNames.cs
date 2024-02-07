namespace ConversorMonedaApi.Data.Models.Dtos
{
    public class ConversionWithCurrencyNames
    {
        public int ConversionId { get; set; }
        public string CurrencyFromName { get; set; }
        public string CurrencyToName { get; set; }
        public double Amount { get; set; }
        public double Result { get; set; }
        public string Date { get; set; }
    }
}
