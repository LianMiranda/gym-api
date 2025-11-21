namespace Gym.Domain.Abstractions.ResultPattern;

public static class ResultExtensions
{
    public static Result<T> ToSuccessResult<T>(this T data)
    {
        return Result<T>.Success(data);
    }
}