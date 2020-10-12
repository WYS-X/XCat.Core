using System;
using System.Collections.Generic;
using System.Text;

public static class StringExtension
{
    public static string EmptyIfNull(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return "";
        else
            return value.Trim();
    }
}