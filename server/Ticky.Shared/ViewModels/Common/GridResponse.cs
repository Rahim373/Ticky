namespace Ticky.Shared.ViewModels.Common;

public class GridResponse<T> : CollectionViewModel
{
    public int Total { get; set; }
    public IList<T> Items { get; set; }
}
