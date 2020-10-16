﻿
    public interface IResult
    {
        int Code { get; set; }
        string Message { get; set; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; set; }
        void Succeed(T data);
}