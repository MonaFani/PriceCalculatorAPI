using PriceCalculatorAPI.Factories;

namespace PriceCalculatorAPI.Helper
{
    public static class VatFactoryHelper
    {
        public static IVATFactory GetVatFActory(string countryidentifier) 
        {
            if (countryidentifier == "AT")
                return new AustriaVATFactory();
            return null;
        }
    }
}
