namespace PriceCalculatorAPI.Models
{
    public class PriceDetail
    {
        public decimal PriceWithoutVAT { get; set; }
        public decimal ValueAddedTax { get; set; }
        public decimal PriceIncludingVAT { get; set; }

    }
}
