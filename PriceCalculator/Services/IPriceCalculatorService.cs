using PriceCalculatorAPI.Models;

namespace PriceCalculatorAPI.Services
{
    public interface IPriceCalculatorService
    {
        PriceDetail Calculate(int VATRate, decimal priceWithoutVAT = 0, decimal valueAddedTax = 0, decimal priceIncludingVAT = 0);
       
        
    }
}
