using System;
using System.Collections.Generic;

public class Result : IResult
{
    public Result() { Code = 0; Message = ""; }
    public Result(int code, string msg = "")
    {
        Code = code;
        Message = msg;
    }

    public int Code { get; set; }
    public string Message { get; set; }

    public static Result Success(string message = "") { return new Result(0, message); }
    public static Result Fail(int code = 1, string message = "") { return new Result(code, message); }
}

public class Result<T> : Result, IResult<T>
{
    public Result() : base() { Data = default; }
    public Result(T data, string message = "") : base(0, message) { Data = data; }
    public Result(int code, string msg = "")
    {
        Code = code;
        Message = msg;
    }
    public T Data { get; set; }

    public new static Result<T> Success(string message = "") { return new Result<T>(0, message); }
    public static Result<T> Success(T value, string message = "") { return new Result<T> { Data = value, Message = message, Code = 0 }; }
    public new static Result<T> Fail(int code = 1, string message = "") { return new Result<T>(code, message); }

    public void Succeed(T data)
    {
        this.Code = 0;
        this.Data = data;
        this.Message = "";
    }
}

public static class ResultExtensions
{
    public static PageResult<TResult> ToPageResult<TSource, TResult>(this PageResult<TSource> model, Func<TSource, TResult> selecter)
    {
        var result = new PageResult<TResult>();
        result.Code = model.Code;
        result.Message = model.Message;
        result.Count = model.Count;
        result.Page = model.Page;
        result.Size = model.Size;
        result.Data = new List<TResult>();
        if (model.Data != null)
        {
            foreach (var item in model.Data)
            {
                result.Data.Add(selecter.Invoke(item));
            }
        }
        return result;
    }

    public static Result<TResult> To<TSource, TResult>(this Result<TSource> model, Func<TSource, TResult> selecter)
    {
        var result = new Result<TResult>();
        result.Code = model.Code;
        result.Message = model.Message;
        if (model.Data == null)
            result.Data = default;
        else
        {
            result.Data = selecter.Invoke(model.Data);
        }
        return result;
    }

    public static TResult ToResult<TSource, TResult>(this TSource model, Func<TSource, TResult> selecter) 
        where TSource : Result where TResult : Result
    {
        var result = selecter.Invoke(model);
        result.Message = model.Message;
        result.Code = model.Code;
        return result;
    }
}
