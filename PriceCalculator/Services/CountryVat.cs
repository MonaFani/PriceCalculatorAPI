using Newtonsoft.Json;

namespace PriceCalculatorAPI.Services
{
    public class CountryVat : ICountryVat
    {
        Dictionary<string, List<int>> countriesVat = new Dictionary<string, List<int>>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CountryVat(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<int> GetVAT(string countryName)
        {

            if (countriesVat.Count == 0)
                countriesVat = GetContriesVat();
            if (countriesVat == null)
                throw new PriceCalculatorNotImplementedException();
            countriesVat.TryGetValue(countryName.ToLower(), out List<int> vatValues);
            if (vatValues == null)
                throw new PriceCalculatorNotImplementedException();
            return vatValues;
        }

        private Dictionary<string, List<int>>? GetContriesVat()
        {
            var rootPath = _webHostEnvironment.ContentRootPath; 

            var fullPath = Path.Combine(rootPath, "CountriesVat.json"); 

            var jsonData = File.ReadAllText(fullPath); 

            if (string.IsNullOrWhiteSpace(jsonData))
                throw new PriceCalculatorNotImplementedException();

            return JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(jsonData);
        }
    }
}
