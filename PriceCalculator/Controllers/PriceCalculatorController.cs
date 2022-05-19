using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PriceCalculatorAPI.Helper;
using PriceCalculatorAPI.Models;
using PriceCalculatorAPI.Services;
using System.Text.RegularExpressions;

namespace PriceCalculatorAPI.Controllers
{
    [Route("api/pricecalculator")]
    [ApiController]
    public class PriceCalculatorController : ControllerBase
    {
        private readonly IPriceCalculatorService _priceCalculatorService;
        private readonly IVATValidator _vATValidator;
 

        public PriceCalculatorController(IPriceCalculatorService priceCalculator, IVATValidator vATValidator)
        {
            _priceCalculatorService = priceCalculator;
            _vATValidator = vATValidator;
            
        }

        [HttpGet]
        public IActionResult Austria(string? country = "AT", string vatRate = " %" , string? priceWithoutVat = "", string? valueAddedTax = "", string? priceIncludingVat = "")
        {

            var factory = VatFactoryHelper.GetVatFActory(country);
            string pattern = @"(\p{Sc})?";
            Regex rgx = new Regex(pattern);
            priceWithoutVat = rgx.Replace(priceWithoutVat ?? "", "");
            valueAddedTax = rgx.Replace(valueAddedTax ?? "", "");
            priceIncludingVat = rgx.Replace(priceIncludingVat ?? "", "");
            if (factory is null)
                throw new PriceCalculatorNotImplementedException();

            var _vatRate = _vATValidator.ValidateVATRate(factory.CreatVATService(), vatRate);
            decimal _priceWithoutVAT = 0;
            decimal _valueAddedTax = 0;
            decimal _priceIncludingVAT = 0;

            if (!string.IsNullOrEmpty(priceWithoutVat) && !Decimal.TryParse(priceWithoutVat, out _priceWithoutVAT))
                throw new PriceCalculatorValidationException("Price Without VAT  rate should be number.");
            if (!string.IsNullOrEmpty(valueAddedTax) && !Decimal.TryParse(valueAddedTax, out _valueAddedTax))
                throw new PriceCalculatorValidationException("VAT Amount should be number.");
            if (!string.IsNullOrEmpty(priceIncludingVat) && !Decimal.TryParse(priceIncludingVat, out _priceIncludingVAT))
                throw new PriceCalculatorValidationException("Price Including VAT should be number.");

            if (_priceWithoutVAT == 0 && _valueAddedTax == 0 && _priceIncludingVAT == 0)
                throw new PriceCalculatorException("Please fill one of price without VAT or VAT Amount or Price Including VAT.");

            List<decimal> prices = new List<decimal> { _priceWithoutVAT, _valueAddedTax, _priceIncludingVAT };
            if (prices.Where(x => x == 0).Count() != 2)
                throw new PriceCalculatorException("Please fill only one of price without VAT or VAT Amount or Price Including VAT.");

            var price = _priceCalculatorService.Calculate(_vatRate, _priceWithoutVAT, _valueAddedTax, _priceIncludingVAT);

            return Ok(price);
        }


    }
}
