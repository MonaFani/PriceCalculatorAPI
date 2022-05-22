

namespace PriceCalculatorAPI.Services
{
    public interface IVatValidator
    {
        void ValidateVatRate(List<int> validVATRate, int VatRate);
    }
}
