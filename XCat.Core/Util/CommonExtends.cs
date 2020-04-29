using System;
using System.Collections.Generic;
using System.Text;

public static class CommonExtends
{
    public static decimal GetPrice(this int? value, int pect = 1)
    {
        if (value == null)
            return 0;
        else
            return (decimal)value.Value / pect;
    }

    public static decimal GetPrice(this long? value, int pect = 1)
    {
        if (value == null)
            return 0;
        else
            return (decimal)value.Value / pect;
    }

    public static decimal GetPrice(this long value, int pect = 1)
    {
        return (decimal)value / pect;
    }

    public static decimal GetPrice(this decimal? value, int pect = 1)
    {
        if (value == null)
            return 0;
        else
            return value.Value / pect;
    }
}