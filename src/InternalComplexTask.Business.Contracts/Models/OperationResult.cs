namespace InternalComplexTask.Business.Contracts.Models
{
    public class OperationResult
    {
        public OperationResult(string message, bool isSucceeded)
        {
            Message = message;
            IsSucceeded = isSucceeded;
        }

        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
