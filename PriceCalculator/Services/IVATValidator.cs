namespace PriceCalculatorAPI.Services
{
    public interface IVATValidator
    {
         int ValidateVATRate(VATService vATService, string VATRate);
      
    }
}
