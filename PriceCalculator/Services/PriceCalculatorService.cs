using PriceCalculatorAPI.Helper;

namespace PriceCalculatorAPI.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        public PriceCalculatedResult Calculate(PriceCalculatorParameter priceCalculatorParameter)
        {
            var result = new PriceCalculatedResult();
            if (priceCalculatorParameter.PriceWithoutVAT != 0) 
            {
                result.ValueAddedTax = PriceCalculatorHelper.CalculateVatAmountByOrginalPrice(priceCalculatorParameter.PriceWithoutVAT, priceCalculatorParameter.VatRate);
                result.PriceIncludingVAT = priceCalculatorParameter.PriceWithoutVAT + result.ValueAddedTax; 
            }
           else if (priceCalculatorParameter.ValueAddedTax != 0)
            {
                result.PriceWithoutVAT = PriceCalculatorHelper.CalculateOriginalPriceByVatAmount(priceCalculatorParameter.ValueAddedTax, priceCalculatorParameter.VatRate);
                result.PriceIncludingVAT = result.PriceWithoutVAT + priceCalculatorParameter.ValueAddedTax;
            }
           else if (priceCalculatorParameter.PriceIncludingVAT != 0)
            {
                result.PriceWithoutVAT = PriceCalculatorHelper.CalculateOriginalPriceByTotalPrice(priceCalculatorParameter.PriceIncludingVAT, priceCalculatorParameter.VatRate);
                result.ValueAddedTax = priceCalculatorParameter.PriceIncludingVAT - result.PriceWithoutVAT;
            }
            return result;
           
        }
       
        
    }

   
}
