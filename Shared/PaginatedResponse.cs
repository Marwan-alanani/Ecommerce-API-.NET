using Shared.DataTransferObjects.Products;

namespace Shared;

public record PaginatedResponse<TData>(int PageIndex, int PageSize, int TotalCount, IEnumerable<TData> data)
{

}