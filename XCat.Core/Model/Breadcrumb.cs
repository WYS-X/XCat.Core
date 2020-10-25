using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 面包屑
/// </summary>
public class Breadcrumbs
{
    public Breadcrumbs(bool showHome = true)
    {
        ShowHome = showHome;
    }
    /// <summary>
    /// 展示首页
    /// </summary>
    public bool ShowHome { get; set; }

    private List<BreadcrumbItem> items { get; set; }
    public List<BreadcrumbItem> Items
    {
        set
        {
            items = value;
        }
        get
        {
            if(items != null)
            {
                var array = new BreadcrumbItem[items.Count];
                items.CopyTo(array, 0);
                var list = array.ToList();
                if (ShowHome)
                {
                    list.Insert(0, new BreadcrumbItem { Title = "首页", Url = "/" });
                }
                return list;
            }
            else
            {
                return items;
            }
        }
    }
}
/// <summary>
/// 面包屑的屑
/// </summary>
public class BreadcrumbItem
{
    public string Title { get; set; }
    public string Url { get; set; }
}
