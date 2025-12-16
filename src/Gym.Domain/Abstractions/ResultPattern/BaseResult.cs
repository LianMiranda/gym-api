namespace Gym.Domain.Abstractions.ResultPattern;

public abstract class BaseResult
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    protected BaseResult(bool isSuccess, string message)
    {
        if (!isSuccess && string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("The error should have a message.", nameof(message));
        
        IsSuccess = isSuccess;
        Message = message;
    }
}
