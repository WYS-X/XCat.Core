using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Request
{
    public static T GetJson<T>(string url)
    {
        var request = WebRequest.CreateHttp(url);
        request.ContentType = "application/json";
        var response = request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            string json = reader.ReadToEnd();
            response.Close();
            return Serializer.FromJson<T>(json);
        }
    }

    public static T GetJson<T,G>(string url, G param)
    {
        foreach (var item in Translater.GetDictionary(param))
        {
            if (url.Contains("?"))
            {

            }
        } 
        return GetJson<T>(url);
    }

    public static string PostJson(string url, IDictionary<string, string> parameters = null, IDictionary<string, string> routeParameters = null)
    {
        if (routeParameters != null)
        {
            if (!url.Contains("?"))
                url += "?";
            foreach (var param in routeParameters)
            {
                url += param.Key + "=" + param.Value + "&";
            }
            url = url.Trim('&');
        }
        var request = WebRequest.CreateHttp(url);
        request.Method = "post";
        request.ContentType = "application/json";
        if (parameters != null)
        {
            using (var writer = request.GetRequestStream())
            {
                byte[] bytes = Translater.GetBytes(parameters);
                writer.Write(bytes, 0, bytes.Length);
            }
        }

        var response = request.GetResponse();
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            string json = reader.ReadToEnd();
            response.Close();
            return json;
        }
    }

    public static T PostJson<T>(string url,IDictionary<string,string> parameters = null,IDictionary<string,string> routeParameters = null)
    {
        return Serializer.FromJson<T>(PostJson(url, parameters, routeParameters));
    }

    public static T PostJson<T>(string url, IDictionary<string, string> parameters)
    {
        return Serializer.FromJson<T>(PostJson(url, parameters));
    }

    public static string GetJson(string url, string method = "GET", string data = "", string encoding = "")
    {
        try
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            if (!string.IsNullOrEmpty(data))
            {
                httpWebRequest.Method = method;
                httpWebRequest.Timeout = 20000;
                using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    sw.Write(data);
                    sw.Close();
                }
            }
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            StreamReader streamReader = null;
            if (encoding == "")
                streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            else
                streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(encoding));
            string json = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            return json;
        }
        catch (Exception ex)
        {
        }
        return "";
    }

    public static T GetJson<T>(string url, string method = "GET", string data = "")
    {
        string rel = GetJson(url, method, data);
        if (string.IsNullOrEmpty(rel))
            return default(T);
        else
            return Serializer.FromJson<T>(rel);
    }
}