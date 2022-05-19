namespace PriceCalculatorAPI
{
    public class PriceCalculatorException : Exception
    {
        public PriceCalculatorException(string message) : base(message)
        {
                
        }
    }

    public class PriceCalculatorValidationException : PriceCalculatorException
    {
        public PriceCalculatorValidationException(string message) : base(message)
        {

        }
    }
    public class PriceCalculatorNotImplementedException : PriceCalculatorException
    {
        public PriceCalculatorNotImplementedException(string message = "The service has not been implemented yet") : base(message)
        {
            
        }
    }



}
