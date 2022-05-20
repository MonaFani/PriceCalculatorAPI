using PriceCalculatorAPI.Factories;

namespace PriceCalculatorAPI.Services
{
    public class VatValidator : IVatValidator
    {
        
        public void ValidateVatRate(VatService service, int VatRate)
        {
            var validVATRate = service.GetVAT();
            if (!validVATRate.Contains(VatRate))
                throw new PriceCalculatorException("VAT rate should be " + String.Join(" or ", validVATRate), nameof(VatRate));
          
        }
    }
}
