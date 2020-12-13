using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public static class ControllerExtensions
{
    public static async Task<List<FileUrl>> SaveFile(this ControllerBase request, string path)
    {
        var currDate = DateTime.Now;
        var root = AppDomain.CurrentDomain.BaseDirectory + "/upload/";
        var filePath = path + "/" + currDate.ToString("yyyyMMdd");
        Util.CreateDirectoryIfEmpty(filePath);
        var files = request.HttpContext.Request.Form.Files;
        var list = new List<FileUrl>();
        foreach (var file in files)
        {
            var saveName = Util.GetNewFileName(file.FileName);
            using (var stream = File.Create(root + filePath + saveName))
            {
                await file.CopyToAsync(stream);
            }
            list.Add(new FileUrl { Name = saveName, Url = filePath + saveName });
        }
        return list;
    }
}