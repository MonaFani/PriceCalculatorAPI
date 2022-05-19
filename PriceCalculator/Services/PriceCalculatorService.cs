using PriceCalculatorAPI.Helper;
using PriceCalculatorAPI.Models;

namespace PriceCalculatorAPI.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        public PriceDetail Calculate(int VATRate, decimal priceWithoutVAT = 0, decimal valueAddedTax = 0, decimal priceIncludingVAT = 0)
        {
            if (priceWithoutVAT!=0) 
            {
                valueAddedTax = PriceCalculatorHelper.CalculateVatAmountByOrginalPrice(priceWithoutVAT, VATRate);
                priceIncludingVAT = priceWithoutVAT + valueAddedTax; 
            }
           else if (valueAddedTax != 0)
            {
                priceWithoutVAT = PriceCalculatorHelper.CalculateOriginalPriceByVatAmount(valueAddedTax, VATRate);
                priceIncludingVAT = priceWithoutVAT + valueAddedTax;
            }
           else if (priceIncludingVAT != 0)
            {
                priceWithoutVAT = PriceCalculatorHelper.CalculateOriginalPriceByTotalPrice(priceIncludingVAT, VATRate);
                valueAddedTax = priceIncludingVAT - priceWithoutVAT;
            }

            return new PriceDetail()
            {
                PriceWithoutVAT = priceWithoutVAT,
                ValueAddedTax = valueAddedTax,
                PriceIncludingVAT = priceIncludingVAT,
            };
        }
       
        
    }

   
}
