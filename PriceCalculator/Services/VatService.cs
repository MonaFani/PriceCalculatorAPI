namespace PriceCalculatorAPI.Services
{
    public class VatService : IVatService
    {
        private readonly ICountryVat _countryVat;

        public VatService(ICountryVat countryVat)
        {
            _countryVat = countryVat;
        }
        public List<int> GetVAT(string countryName)
        {
            return _countryVat.GetVAT(countryName);
        }
    }
}
