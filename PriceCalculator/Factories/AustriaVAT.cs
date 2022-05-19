namespace PriceCalculatorAPI.Factories
{
    public class AustriaVATService : VATService
    {
        public AustriaVATService()
        {
            validVAT.Add(10);
            validVAT.Add(13);
            validVAT.Add(20);
        }


    }

    public class AustriaVATFactory : IVATFactory
    {

        public VATService CreatVATService()
        {
            return new AustriaVATService();
        }

    }
}
