namespace PriceCalculatorAPI.Factories
{
    public class AustriaVATService : VatService
    {
        public AustriaVATService()
        {
            validVAT.Add(10);
            validVAT.Add(13);
            validVAT.Add(20);
        }


    }

    public class AustriaVatFactory : IAustriaVatFactory
    {

        public VatService CreateVATService()
        {
            return new AustriaVATService();
        }

    }
}
