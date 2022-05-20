using PriceCalculatorAPI.Helper;

namespace PriceCalculatorAPI.Services
{
    public interface IPriceCalculatorService
    {
        PriceCalculatedResult Calculate(PriceCalculatorParameter priceDetail);
       
        
    }
}
