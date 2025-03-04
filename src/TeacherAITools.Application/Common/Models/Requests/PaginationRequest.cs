using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using TeacherAITools.Application.Common.Constants;
using TeacherAITools.Application.Common.Enums;

namespace TeacherAITools.Application.Common.Models.Requests
{
    public abstract class PaginationRequest<T> where T : class
    {
        private int _pageNumber = DefaultPagination.DefaultPageNumber;

        private int _pageSize = DefaultPagination.DefaultPageSize;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 0
                ? value
                : DefaultPagination.DefaultPageNumber;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 && value <= DefaultPagination.MaxPageSize
                ? value
                : DefaultPagination.DefaultPageSize;
        }

        public string? SortColumn { get; set; }

        public SortDirection SortDir { get; set; } = SortDirection.Asc;

        public abstract Expression<Func<T, bool>> GetExpressions();

        public Func<IQueryable<T>, IOrderedQueryable<T>>? GetOrder()
        {
            if (string.IsNullOrWhiteSpace(SortColumn)) return null;

            return query => query.OrderBy($"{SortColumn} {SortDir.ToString().ToLower()}");
        }
    }
}
