using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class Translater
{
    public static Dictionary<string, string> GetDictionary<T>(T obj)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        Type type = obj.GetType();
        PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var p in pi)
        {
            MethodInfo methodInfo = p.GetGetMethod();
            if (methodInfo != null && methodInfo.IsPublic)
            {
                var value = methodInfo.Invoke(obj, new object[] { });
                if (value != null)
                    dic.Add(p.Name, value.ToString());
            }
        }
        return dic;
    }

    public static byte[] GetBytes(object obj)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            return ms.GetBuffer();
        }
    }

    
}