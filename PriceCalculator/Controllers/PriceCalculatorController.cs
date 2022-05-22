using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceCalculatorAPI.Helper;
using PriceCalculatorAPI.Services;
using System.Dynamic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace PriceCalculatorAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/pricecalculator")]
    [ApiController]
    [ResponseCache(NoStore = true)]
    public class PriceCalculatorController : ControllerBase
    {
        private readonly IPriceCalculatorService _priceCalculatorService;
        private readonly IVatValidator _vATValidator;
        private readonly IVatService _vatService;

        public PriceCalculatorController(IPriceCalculatorService priceCalculator, IVatValidator vATValidator, IVatService vatService)
        {
            _priceCalculatorService = priceCalculator;
            _vATValidator = vATValidator;
            _vatService = vatService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetCalculatedPrice([FromQuery] PriceCalculatorParameters priceCalculatorParameter)
        {
            _vATValidator.ValidateVatRate(_vatService.GetVAT(), priceCalculatorParameter.VatRate);

            if (priceCalculatorParameter.PriceWithoutVAT == 0 && priceCalculatorParameter.ValueAddedTax == 0 && priceCalculatorParameter.PriceIncludingVAT == 0)
                throw new PriceCalculatorException("Please fill one of price without VAT or VAT Amount or Price Including VAT.", nameof(PriceCalculatorParameters));

            var prices = new List<decimal> { priceCalculatorParameter.PriceWithoutVAT, priceCalculatorParameter.ValueAddedTax, priceCalculatorParameter.PriceIncludingVAT };
            if (prices.Where(x => x == 0).Count() != 2)
                throw new PriceCalculatorException("Please fill only one of price without VAT or VAT Amount or Price Including VAT.", nameof(PriceCalculatorParameters));

            var calculatedResult = _priceCalculatorService.Calculate(priceCalculatorParameter).ShapeData(Decimal.Zero).ToJsonCamelCase();
            return Ok(calculatedResult);
        }
    }
}
