

namespace PriceCalculatorAPI.Services
{
    public class VatValidator : IVatValidator
    {
        
        public void ValidateVatRate(List<int> validVATRate, int VatRate)
        {
          
            if (!validVATRate.Contains(VatRate))
                throw new PriceCalculatorException("VAT rate should be " + String.Join(" or ", validVATRate), nameof(VatRate));
          
        }
    }
}
