namespace Gym.Domain.Abstractions.ResultPattern;

public static class ResultExtensions
{
    public static ResultData<T> ToSuccessResult<T>(this T data)
    {
        return ResultData<T>.Success(data);
    }
}