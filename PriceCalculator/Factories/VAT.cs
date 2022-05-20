namespace PriceCalculatorAPI.Factories
{
    public abstract class VatService
    {
        protected List<int> validVAT = new List<int>();
        public virtual List<int> GetVAT()
        {
            return validVAT;
        }
    }
    public interface IVatFactory
    {
        VatService CreateVATService();
    }

    public interface IAustriaVatFactory : IVatFactory 
    { 
    
    }


}
