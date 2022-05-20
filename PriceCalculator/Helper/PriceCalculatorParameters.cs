using System.ComponentModel.DataAnnotations;

namespace PriceCalculatorAPI.Helper
{
    public class PriceCalculatorParameters
    {
        public int VatRate { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public decimal ValueAddedTax { get; set; }
        public decimal PriceIncludingVAT { get; set; }

    }
}
