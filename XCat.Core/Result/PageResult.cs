using System;
using System.Collections.Generic;
using System.Linq;

public class PageResult<T> : Result
{
    public PageResult() : base()
    {
        this.Data = new List<T>();
    }
    public PageResult(int code, string msg = ""): base(code, msg)
    {

    }

    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages
    {
        get
        {
            if (Size == 0)
                return 0;
            return (int)Math.Ceiling((double)Count / Size);
        }
    }

    public bool HasPrevious => (Index > 0);

    public bool HasNext => (Index + 1 < Pages);
    public List<T> Data { get; set; }
}

public static class PageResultExtension
{
    public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        return new PageResult<T>
        {
            Count = query.Count(),
            Index = pageIndex,
            Size = pageSize,
            Data = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
        };
    }

    public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, SearchBase request)
    {
        return query.ToPageResult(request.Index, request.Size);
    }

    public static PageResult<target> Translate<curr, target>(this PageResult<curr> source, Func<curr, target> func)
    {
        var res = new PageResult<target>()
        {
            Index = source.Index,
            Count = source.Count,
            Size = source.Size,
            Data = source.Data.Select(x => func(x)).ToList()
        };
        return res;
    }
}
