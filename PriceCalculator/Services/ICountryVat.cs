namespace PriceCalculatorAPI.Services
{
    public interface ICountryVat
    {
        List<int> GetVAT(string countryName);
    }
}
