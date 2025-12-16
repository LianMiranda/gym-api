namespace Gym.Application.Dtos.Base;

public abstract record PagedResponse<T>
{
    public List<T>? Response { get; set; } 
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
};