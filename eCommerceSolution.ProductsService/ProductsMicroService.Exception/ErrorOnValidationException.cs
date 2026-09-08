namespace ProductsMicroService.Exception;

public class ErrorOnValidationException : ProductsException
{
    public IList<string> ErrorMessages { get; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}