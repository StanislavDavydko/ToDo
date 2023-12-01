namespace ToDo.Web.Shared
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
