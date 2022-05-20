using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceCalculatorAPI.Factories;
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
    public class PriceCalculatorController : ControllerBase
    {
        private readonly IPriceCalculatorService _priceCalculatorService;
        private readonly IVatValidator _vATValidator;
        private readonly IAustriaVatFactory _austriaVatFactory;

        public PriceCalculatorController(IPriceCalculatorService priceCalculator, IVatValidator vATValidator, IAustriaVatFactory austriaVatFactory)
        {
            _priceCalculatorService = priceCalculator;
            _vATValidator = vATValidator;
            _austriaVatFactory = austriaVatFactory;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult GetCalculatedPrice([FromQuery] PriceCalculatorParameters priceCalculatorParameter)
        {


            _vATValidator.ValidateVatRate(_austriaVatFactory.CreateVATService(), priceCalculatorParameter.VatRate);

            if (priceCalculatorParameter.PriceWithoutVAT == 0 && priceCalculatorParameter.ValueAddedTax == 0 && priceCalculatorParameter.PriceIncludingVAT == 0)
                throw new PriceCalculatorException("Please fill one of price without VAT or VAT Amount or Price Including VAT.");

            var prices = new List<decimal> { priceCalculatorParameter.PriceWithoutVAT, priceCalculatorParameter.ValueAddedTax, priceCalculatorParameter.PriceIncludingVAT };
            if (prices.Where(x => x == 0).Count() != 2)
                throw new PriceCalculatorException("Please fill only one of price without VAT or VAT Amount or Price Including VAT.");

            var result = _priceCalculatorService.Calculate(priceCalculatorParameter);
            dynamic returnObj = result.ShapeData(Decimal.Zero);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            };
            var jsonString = JsonSerializer.Serialize(returnObj, serializeOptions);

            return Ok(jsonString);
        }
    }
}
