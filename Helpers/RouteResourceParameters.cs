using System.ComponentModel;

namespace FunWithFlights.Helpers
{
    public class RouteResourceParameters
    {
        const int maxPageSize = 100;
        const int defaultPageNumber = 1;
        const int defaultPageSize = 10;

        private int _pageSize = defaultPageSize;
        [DefaultValue(defaultPageSize)]
        public int PageSize {
            get => _pageSize;
            set => _pageSize = Math.Min(maxPageSize, value);
        }
        [DefaultValue(defaultPageNumber)]
        public int PageNumber { get; set; } = defaultPageNumber;
    }
}
