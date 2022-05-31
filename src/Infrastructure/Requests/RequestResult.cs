namespace Infrastructure.Requests;

public class RequestResult<T>
{
    public T? Value { get; set; }
    public RequestResultStatus Status { get; set; }
    public string? Message { get; set; }

    public static RequestResult<T> Success(T value)
       => new()
       {
           Value = value,
           Status = RequestResultStatus.SUCCESS
       };

    public static RequestResult<T> Null()
        => new()
        {
            Message = "Object not found!",
            Status = RequestResultStatus.NOT_FOUND
        };

    public static RequestResult<T> Error(string error)
        => new()
        {
            Message = error,
            Status = RequestResultStatus.VALIDATION_ERROR
        };
}

public class EmptyRequestResult
{
    public RequestResultStatus Status { get; set; }
    public string? Message { get; set; }

    public static EmptyRequestResult Success()
       => new()
       {
           Status = RequestResultStatus.SUCCESS
       };

    public static EmptyRequestResult Null()
        => new()
        {
            Message = "Object not found!",
            Status = RequestResultStatus.NOT_FOUND
        };
}

public enum RequestResultStatus
{
    SUCCESS,
    VALIDATION_ERROR,
    NOT_FOUND
}
