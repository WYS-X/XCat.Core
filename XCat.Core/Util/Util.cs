using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public static class Util
{
    public static string GetRandomCode(int start = 1000, int end = 9999)
    {
        return new Random((int)DateTime.Now.Ticks).Next(start, end).ToString();
    }
    public static int GetRandom(int length = 4)
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        int number = random.Next((int)(length * Math.Pow((double)10, (double)(length - 1))), (int)Math.Pow(10.0, (double)length) - 1);
        return number;
    }
    public static string GetToken()
    {
        return Guid.NewGuid().ToString("N");
    }
    /// <summary>
    /// 随机密码
    /// </summary>
    /// <returns></returns>
    public static string GetPassword()
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        string password = random.Next(10000000, 99999999).ToString();
        password = MD5(password);
        return password;
    }
    public static string GetPassword(string str)
    {
        return MD5(str, 3);
    }

    public static string MD5(string str, int times)
    {
        for (int i = 0; i < times; i++)
        {
            str = MD5(str).ToLower();
        }
        return str;
    }

    public static bool IsMobile(string mobile)
    {
        Regex regex = new Regex(@"^(13[0-9]|15[012356789]|17[0-9]|18[0-9]|14[57])[0-9]{8}$");
        if (regex.IsMatch(mobile))
            return true;
        else
            return false;
    }
    /// <summary>
    /// 模糊手机号
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string ObscureMobile(string mobile)
    {
        if (string.IsNullOrEmpty(mobile))
            return "";
        mobile = mobile.Trim().Remove(3, 4);
        return mobile.Insert(3, "****");
    }
    /// <summary>
    /// 模糊身份证号
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string ObscureIdNumber(string idNumber)
    {
        if (string.IsNullOrEmpty(idNumber))
            return "";
        idNumber = idNumber.Trim().Remove(3, 12);
        return idNumber.Insert(3, "************");
    }
    public static bool IsEmail(string email)
    {
        Regex regex = new Regex(@"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");
        if (regex.IsMatch(email))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 登陆端名称
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public static string GetLoginType(int clientId)
    {
        string type = "";
        switch (clientId)
        {
            case 1: type = "微信"; break;
            case 2: type = "IOS"; break;
            case 3: type = "Android"; break;
            case 4: type = "师傅"; break;
            case 5: type = "咨询师"; break;
            case 6: type = "商城"; break;
            case 9: type = "老板App"; break;
            case 11: type = "售前App"; break;
            case 13: type = "供应商公众号"; break;
            default: type = "其他"; break;
        }
        return type;
    }

    public static string MD5(string str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] bytes = md5.ComputeHash(Encoding.Default.GetBytes(str));
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    public static string Resize(string url, string m, int w = 0, int h = 0)
    {
        if (url.Contains("360buyimg.com"))
            return url;
        string rel = "";
        if (url.Contains("?"))
            rel = url + "&x-oss-process=image/resize,";
        else
            rel = url + "?x-oss-process=image/resize,";
        if (!string.IsNullOrEmpty(m))
            rel += "m_" + m;
        if (w > 0)
            rel += ",w_" + w;
        if (h > 0)
            rel += ",h_" + h;
        return rel;
    }
    /// <summary>
    /// 从字符串中重置图片尺寸
    /// </summary>
    /// <param name="str"></param>
    /// <param name="m"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public static string ResizeFromString(string str, string m, int w, int h)
    {
        Regex regex = new Regex(@"http://image.it-hdz.com/[\w/-]{1,}.(jpg|png){1}");
        MatchCollection mc = regex.Matches(str);
        if (mc.Count > 0)
        {
            foreach (Match item in mc)
            {
                str = str.Replace(item.Value, Resize(item.Value, m, w, h));
            }
        }
        return str;
    }

    //public static bool CreateQrCode(string path, string content)
    //{
    //    try
    //    {
    //        QRCodeGenerator qrGenerator = new QRCodeGenerator();
    //        QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
    //        QRCode qrCode = new QRCode(qrCodeData);
    //        Bitmap qrCodeImage = qrCode.GetGraphic(20);
    //        qrCodeImage.Save(path, ImageFormat.Png);
    //        qrCodeImage.Dispose();
    //        return true;
    //    }
    //    catch(Exception ex)
    //    {
    //        NLoghelper.Error(ex, "生成二维码失败");
    //        return false;
    //    }
    //}
    //public static bool CreateBarCode(string path, string content)
    //{
    //    try
    //    {
    //        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
    //        Image img = b.Encode(BarcodeLib.TYPE.CODE128, content, Color.Black, Color.White, 290, 120);
    //        img.Save(path, ImageFormat.Png);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        NLoghelper.Error(ex, "生成条形码失败");
    //        return false;
    //    }
    //}

    /// <summary>
    /// 明天时间，不包含时间部分
    /// 示例：2020/4/29
    /// </summary>
    /// <returns></returns>
    public static DateTime GetTomorrow()
    {
        var dt = DateTime.Now.AddDays(1);
        return new DateTime(dt.Year, dt.Month, dt.Day);
    }
}