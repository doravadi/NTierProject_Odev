namespace Project.Bll.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success(string message = "İşlem başarılı")
        {
            return new Result(true, message);
        }

        public static Result Failure(string message = "İşlem başarısız")
        {
            return new Result(false, message);
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public Result(bool isSuccess, string message, T? data) : base(isSuccess, message)
        {
            Data = data;
        }

        public static Result<T> Success(T data, string message = "İşlem başarılı")
        {
            return new Result<T>(true, message, data);
        }

        public static new Result<T> Failure(string message = "İşlem başarısız")
        {
            return new Result<T>(false, message, default);
        }
    }
}