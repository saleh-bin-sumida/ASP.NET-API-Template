namespace ASP.NET_API_Template.Core.Models.Response;

public class PagedResult<T>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    public List<T> Items { get; set; }
    public bool HasNextPage => PageSize * PageNumber < TotalItems;
    public bool HasPreveiosPage => PageNumber > 1;


    public PagedResult()
    {

    }

    public PagedResult(int pageSize, int pageNumber, int totalItems, List<T> items)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalItems = totalItems;
        Items = items;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static PagedResult<T> Create(int pageSize, int pageNumber, int totalRecords, List<T> items)
    {
        return new PagedResult<T>(pageSize, pageNumber, totalRecords, items);
    }


}
