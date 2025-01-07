namespace DevFreela.Application.Models;

public class ResultViewModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    
    public ResultViewModel(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    
    public static ResultViewModel Success() => new (true, "");
    
    public static ResultViewModel Error(string message) => new (false, message);
}

public class ResultViewModel<T> : ResultViewModel
{
    public T? Data { get; set; }
    
    public ResultViewModel(T data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }

    public static ResultViewModel<T> Success(T data) => new (data);
    
    public new static ResultViewModel<T> Error(string message) => new (default!, false, message);
}