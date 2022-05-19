namespace PriceCalculatorAPI.Services
{
    public class VATValidator : IVATValidator
    {
        
        public int ValidateVATRate(VATService service, string VATRate)
        {
            int _rate = 0;
            var validVATRate = service.GetVAT();
            if (!string.IsNullOrEmpty(VATRate) && !Int32.TryParse(VATRate.Replace("%",""), out _rate))
                throw new PriceCalculatorValidationException("VAT rate should be a number");
            else if (!validVATRate.Contains(_rate))
                throw new PriceCalculatorException("VAT rate should be " + String.Join(" or ",validVATRate));
            return _rate;
        }
    }
}
