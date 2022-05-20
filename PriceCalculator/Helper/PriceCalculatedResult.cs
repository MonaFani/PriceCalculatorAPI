namespace PriceCalculatorAPI.Helper
{
    public class PriceCalculatedResult
    {
        public decimal PriceWithoutVAT { get; set; }
        public decimal ValueAddedTax { get; set; }
        public decimal PriceIncludingVAT { get; set; }
    }
}
