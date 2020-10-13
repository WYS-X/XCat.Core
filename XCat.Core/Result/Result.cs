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

    public void Fail(int code = 1, string message = "")
    {
        this.Code = code;
        this.Message = message;
    }

    public void Succeed(string message = "")
    {
        this.Code = 0;
        this.Message = message;
    }
}

public class Result<T> : Result, IResult<T>
{
    public Result() : base() { Data = default; }
    public Result(T data, string msessage = "") : base(0, msessage) { Data = data; }
    public T Data { get; set; }

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
        result.Index = model.Index;
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
