namespace PriceCalculatorAPI.Factories
{
    public abstract class VATService
    {
        protected List<int> validVAT = new List<int>();
        public virtual List<int> GetVAT()
        {
            return validVAT;
        }

        public virtual bool VATIsValid(int vat)
        {
            if (vat == 0)
                throw new PriceCalculatorValidationException("the VAT can not be 0.");
            if (!validVAT.Contains(vat))
                throw new PriceCalculatorValidationException("the VAT is not valid.");
            return true;
        }
    }
    public interface IVATFactory
    {
        VATService CreatVATService();
    }


}
