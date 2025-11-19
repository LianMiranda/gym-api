namespace Gym.Domain.Abstractions.ResultPattern;

public class ResultData<T> : Result
{
    public T? Data { get; private set; }

    public ResultData(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }
    
    public static ResultData<T> Success(T data) => new(data);
    public static ResultData<T> Error(string message) => new(default, false, message);
}