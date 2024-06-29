namespace POSystem.Domain.Entities;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int PageIndex { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (TotalItems + PageSize - 1) / PageSize;
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
}
