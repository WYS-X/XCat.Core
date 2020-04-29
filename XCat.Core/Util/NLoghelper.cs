using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class NLoghelper
{
    static NLoghelper()
    {
        var config = new LoggingConfiguration();

        var traceTarget = new FileTarget
        {
            Layout = "${longdate}【${level}】【${logger}】：${message} ${newline}${onexception:inner=${newline} *****Error***** ${newline} ${exception:format=toString}${exception:format=StackTrace}}",
            Encoding = Encoding.UTF8,
            FileName = "${basedir}/logs/${date:format=yyyyMM}/${shortdate}_trace.log"
        };
        config.AddRule(LogLevel.Trace, LogLevel.Trace, traceTarget);

        var errorTarget = new FileTarget
        {
            Layout = "${longdate}【${level}】【${logger}】：${message} ${newline}${onexception:inner=${newline} *****Error***** ${newline} ${exception:format=toString}${exception:format=StackTrace}}",
            Encoding = Encoding.UTF8,
            FileName = "${basedir}/logs/${date:format=yyyyMM}/${shortdate}_error.log"
        };
        config.AddRule(LogLevel.Error, LogLevel.Error, errorTarget);

        LogManager.Configuration = config;
    }

    static Logger GetLogger()
    {
        StackTrace trace = new StackTrace();
        if (trace == null)
            return LogManager.GetLogger($"{trace.GetFrame(2).GetMethod().DeclaringType.Name}.{trace.GetFrame(2).GetMethod().Name}");
        else
            return LogManager.GetLogger("xcat");
    }

    public static void Error(Exception ex, string message = "")
    {
        var logger = GetLogger();
        logger.Error(ex, message);
    }
    /// <summary>
    /// 输出对象信息
    /// </summary>
    /// <typeparam name="t">对象信息</typeparam>
    /// <param name="message">消息</param>
    public static void Trace<T>(T t, string message = "")
    {
        var logger = GetLogger();
        logger.Trace($"{(string.IsNullOrEmpty(message) ? "" : message + "\n\t")} {Serializer.ToJson(t)}");
    }
    public static void Trace(string message)
    {
        var logger = GetLogger();
        logger.Trace(message);
    }
}