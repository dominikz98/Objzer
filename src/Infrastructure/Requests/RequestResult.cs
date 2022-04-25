namespace Infrastructure.Requests
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

        public static RequestResult<T> Null<T>()
            => new()
            {
                Message = "Object not found!",
                Status = RequestResultStatus.NOT_FOUND
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
        VALIDATION_ERROR,
        NOT_FOUND
    }
}
