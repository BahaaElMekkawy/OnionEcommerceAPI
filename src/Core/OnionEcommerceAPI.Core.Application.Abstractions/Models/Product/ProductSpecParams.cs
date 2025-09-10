namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Product
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public string? Search { get; set; }

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value > 0 ? value : 1; }
        }
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? pageSize : value; }
        }

        private const int MaxPageSize = 10;
        private int pageSize = 5;
        private int pageIndex = 1;


    }
}
