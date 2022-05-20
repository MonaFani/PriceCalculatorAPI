using System.ComponentModel.DataAnnotations;

namespace PriceCalculatorAPI.Helper
{
    public class PriceCalculatorParameter
    {

      
        public int VatRate { get; set; }
        public decimal PriceWithoutVAT { get; set; }
        public decimal ValueAddedTax { get; set; }
        public decimal PriceIncludingVAT { get; set; }

    }
}
