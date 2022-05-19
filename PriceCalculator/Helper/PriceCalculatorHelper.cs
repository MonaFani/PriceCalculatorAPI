namespace PriceCalculatorAPI.Helper
{
    public static class PriceCalculatorHelper
    {
        public static decimal CalculateOriginalPriceByVatAmount(decimal valueAddedTax, int VATRate)
        {
            return decimal.Round((valueAddedTax * 100) / VATRate,2);

        }
        public static decimal CalculateVatAmountByOrginalPrice(decimal priceWithoutVAT, int VATRate)
        {
            return decimal.Round((VATRate * priceWithoutVAT) / 100, 2);
        }
        public static decimal CalculateOriginalPriceByTotalPrice(decimal priceIncludingVAT, int VATRate)
        {
            return decimal.Round((priceIncludingVAT * 100) / (100 + VATRate), 2);

        }

    }
}
