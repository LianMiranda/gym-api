namespace Gym.Domain.Abstractions.ResultPattern;

public class Result<T> : BaseResult
{
    public T? Data { get; private set; }

    public Result(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new(data);
    public static Result<T> Error(string message) => new(default, false, message);
}

//Result without data
public class Result : Result<object>
{
    public Result(bool isSuccess, string message) : base(null, isSuccess, message)
    {
    }

    public static Result Success(string message = "") => new(true, message);
    public static Result Error(string message) => new(false, message);
}