using System;
using System.Collections.Generic;
using System.Text;

public static class StringExtensions
{
    /// <summary>
    /// null则为empty，并去除前后空格
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string TrimAndEmpty(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";
        return value.Trim();
    }
}