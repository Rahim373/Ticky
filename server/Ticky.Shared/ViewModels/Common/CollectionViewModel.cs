namespace Ticky.Shared.ViewModels.Common;

public abstract class CollectionViewModel
{
    public int PageSize { get; set; } = 15;
    public int PageNumber { get; set; } = 1;
}
