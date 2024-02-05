using InsaClub.Models;

namespace InsaClub.ViewModels;

public class IndexEventViewModel
{
    public IEnumerable<Event> Events { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalEvents { get; set; }
    public int Category { get; set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}