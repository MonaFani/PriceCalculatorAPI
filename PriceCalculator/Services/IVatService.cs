namespace PriceCalculatorAPI.Services
{
    public interface IVatService
    {
        List<int> GetVAT(string countryName = "austria");
    }
}