namespace PriceCalculatorAPI
{
    public class PriceCalculatorException : Exception
    {
        public readonly string Field;

        public PriceCalculatorException(string message, string? field = null) : base(message)
        {
            Field = field ?? "Error";
        }
    }

    public class PriceCalculatorValidationException : PriceCalculatorException
    {
        
        public PriceCalculatorValidationException(string message,string? field = null) : base(message,field)
        {

        }
    }
    public class PriceCalculatorNotImplementedException : PriceCalculatorException
    {
        public PriceCalculatorNotImplementedException(string message = "The service has not been implemented yet",string? field = null) : base(message,field)
        {
            
        }
    }



}
