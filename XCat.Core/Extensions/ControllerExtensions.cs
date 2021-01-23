using Microsoft.AspNetCore.Http;
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
    public static async Task<List<FileUrl>> SaveFile(this HttpRequest request, string baseDir, string path)
    {
        var list = new List<FileUrl>();
        var currDate = DateTime.Now;
        var formData = request.Form;
        var root = baseDir;
        var filePath = formData["path"] + "/" + path + currDate.ToString("yyyyMMdd") + "/";
        Util.CreateDirectoryIfEmpty(root + filePath);

        foreach (var file in formData.Files)
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