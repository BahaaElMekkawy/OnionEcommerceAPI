namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Product
{
    public class PagedResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public required IEnumerable<T> Data { get; set; }
    }
}
