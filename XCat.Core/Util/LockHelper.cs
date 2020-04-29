using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 处理关键操作的并发问题
/// 比如：拆分订单的并发
/// </summary>
public class LockHelper
{
    private static List<OperationLock> _locks = new List<OperationLock>();

    /// <summary>
    /// 超时秒数
    /// </summary>
    private static int _timeouts = 10;

    static string GetKey(string locks, string id)
    {
        return $"{locks.Trim()}_{id}";
    }

    /// <summary>
    /// 设置锁
    /// </summary>
    /// <param name="locker">锁名称</param>
    /// <param name="id">锁定值</param>
    /// <param name="timeouts">超时设置，默认10秒</param>
    /// <returns>false:已存在锁</returns>
    public static bool SetLock(string locks, string id, int timeouts = 10)
    {
        if (string.IsNullOrEmpty(locks) || string.IsNullOrEmpty(id))
            throw new Exception("未设定锁名称或锁定值");
        ClearTimeouts();
        var key = GetKey(locks, id);
        if (_locks.Any(x => x.key == key))
            return false;
        else
        {
            if (timeouts <= 0)
                timeouts = _timeouts;
            _locks.Add(new OperationLock { key = key, Value = id, Expires = DateTime.Now.AddSeconds(timeouts) });
            return true;
        }
    }
    /// <summary>
    /// 设置锁
    /// </summary>
    /// <param name="locker">锁名称</param>
    /// <param name="id">锁定数值</param>
    /// <param name="timeouts">超时设置，默认10秒</param>
    /// <returns>false:已存在锁</returns>
    public static bool SetLock(string locks, int id, int timeouts = 10)
    {
        return SetLock(locks, id.ToString(), timeouts);
    }

    /// <summary>
    /// 删除锁
    /// </summary>
    /// <param name="locks">锁名称</param>
    /// <param name="id">锁定值</param>
    public static void DeleteLock(string locks, string id)
    {
        _locks.RemoveAll(x => x.key == GetKey(locks, id));
    }
    /// <summary>
    /// 删除锁
    /// </summary>
    /// <param name="locks">锁名称</param>
    /// <param name="id">锁定值</param>
    public static void DeleteLock(string locks, int id)
    {
        DeleteLock(locks, id.ToString());
    }

    /// <summary>
    /// 清除过期锁
    /// </summary>
    /// <returns></returns>
    static int ClearTimeouts()
    {
        return _locks.RemoveAll(x => x.Expires < DateTime.Now);
    }
}

public class OperationLock
{
    public string key { get; set; }
    public string Value { get; set; }
    public DateTime Expires { get; set; }
}