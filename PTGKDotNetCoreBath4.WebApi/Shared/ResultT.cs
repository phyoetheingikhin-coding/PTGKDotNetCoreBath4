namespace PTGKDotNetCoreBath4.WebApi.Shared
{
    public class ResultT<T> : Result
    {
        public T? Data { get; }

        public Result(bool isSuccess, Error error, T? data) : base(isSuccess, error)
        {
            Data = data;
        }
    }
}
