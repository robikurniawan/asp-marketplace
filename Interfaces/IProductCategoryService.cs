using AspMarketplace.Web.DTOs;
using AspMarketplace.Web.ViewModels.Admin;

namespace AspMarketplace.Web.Interfaces;

public interface IProductCategoryService
{
    Task<PaginatedResult<ProductCategoryRowViewModel>> GetPagedAsync(string? search, int page, int pageSize = 10);
    Task<ProductCategoryFormViewModel?> GetByIdAsync(Guid id);
    Task CreateAsync(ProductCategoryFormViewModel vm);
    Task<bool> UpdateAsync(Guid id, ProductCategoryFormViewModel vm);
    Task<bool> DeleteAsync(Guid id);
}
