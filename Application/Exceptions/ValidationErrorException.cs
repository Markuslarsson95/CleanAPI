namespace Application.Exceptions
{
    public class ValidationErrorException : BaseCustomException
    {
        public List<string> ValidationErrors { get; }
        public ValidationErrorException(object dto, List<string> validationErrors) : base($"Validation of {dto.GetType().Name} failed")
        {
            ValidationErrors = validationErrors ?? new List<string>();
        }
    }
}
