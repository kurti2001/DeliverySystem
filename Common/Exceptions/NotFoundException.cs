namespace Common.Exceptions
{
    public class NotFoundException : DeliverySystemException
    {
        public NotFoundException(string message ="Entity not found") : base(message)
           
        { 

        }
    }
}
