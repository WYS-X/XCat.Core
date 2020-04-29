
    public interface IResult
    {
        int Code { get; set; }
        string Message { get; set; }
        void Succeed(string message = "");
        void Fail(int code = 1, string message = "");
    }

    public interface IResult<T> : IResult
    {
        T Data { get; set; }
        void Succeed(T data);
}