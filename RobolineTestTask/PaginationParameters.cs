using System.ComponentModel;

namespace RobolineTestTask
{
    public class PaginationParameters
    {
        private const int maxPageSize = 20;

        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount {  get; }
        public int TotalPages { get; }

        public PaginationParameters(int pageNumber, int pageSize, int totalCount)
        {
            if (pageSize < 1 || pageSize > maxPageSize)
                throw new ArgumentException($"Invalid page size: value must be from 1 to {maxPageSize}");

            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);

            if (pageNumber < 1 || pageNumber > TotalPages)
                throw new ArgumentException($"Invalid page number: value must be from 1 to {TotalPages}");

            PageNumber = pageNumber;
        }
    }
}
