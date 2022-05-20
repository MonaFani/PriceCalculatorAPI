using PriceCalculatorAPI.Factories;

namespace PriceCalculatorAPI.Services
{
    public interface IVatValidator
    {
        void ValidateVatRate(VatService vATService, int VatRate);
      
    }
}
