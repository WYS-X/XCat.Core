using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Text;

/// <summary>
/// 通用对象比较器
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="V"></typeparam>
public class Comparer<T, V> : IEqualityComparer<T>
{
    Func<T, V> keySelector;
    public Comparer(Func<T, V> func)
    {
        keySelector = func;
    }

    public bool Equals(T x, T y)
    {
        if (x == null || y == null)
        {
            return false;
        }
        return keySelector(x).Equals(keySelector(y));
    }

    public int GetHashCode(T obj)
    {
        if (obj == null)
            return 0;
        return obj.GetHashCode();
    }
}