﻿using System;
using System.Collections.Generic;
using System.Text;

public static class DecimalExtensions
{
    /// <summary>
    /// 价格保留两位小数
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static decimal Price(this decimal value)
    {
        return value.Round();
    }
    /// <summary>
    /// 四舍五入
    /// </summary>
    /// <param name="value"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static decimal Round(this decimal value, int num = 2)
    {
        return Math.Round(value, num, MidpointRounding.AwayFromZero);
    }
}