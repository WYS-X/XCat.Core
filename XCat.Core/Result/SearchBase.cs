
using static OrderByExpression;

public class SearchBase
{
    public SearchBase()
    {
        _size = 10;
        Index = 1;
        Text = string.Empty;
    }

    private int _size;

    public int Index { get; set; }

    public int Size
    {
        get { return _size; }
        set
        {
            if (value > 50)
            {
                _size = 50;
            }
            else
            {
                _size = value;
            }
        }
    }

    public string Text { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    public string Sort { get; set; }
    /// <summary>
    /// 排序方式
    /// </summary>
    public SortDirection SortOrder { get; set; }
}