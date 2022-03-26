namespace api.Core
{
    public class RequestResult<T>
    {
        public T? Value { get; set; }
        public RequestResultStatus Status { get; set; }
        public string? Message { get; set; }
    }

    internal static class RequestResult
    {
        public static RequestResult<T> Success<T>(T value)
            => new()
            {
                Value = value,
                Status = RequestResultStatus.SUCCESS
            };

        public static RequestResult<T> Error<T>(string error)
            => new()
            {
                Message = error,
                Status = RequestResultStatus.VALIDATION_ERROR
            };
    }

    public enum RequestResultStatus
    {
        SUCCESS,
        VALIDATION_ERROR
    }
}
