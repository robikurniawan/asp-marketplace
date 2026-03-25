using AspMarketplace.Web.DTOs;
using AspMarketplace.Web.Interfaces;
using AspMarketplace.Web.Models;
using AspMarketplace.Web.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace AspMarketplace.Web.Services;

public class ProductCategoryService(IUnitOfWork uow, ICurrentUserService currentUser) : IProductCategoryService
{
    public async Task<PaginatedResult<ProductCategoryRowViewModel>> GetPagedAsync(string? search, int page, int pageSize = 10)
    {
        var query = uow.Repository<ProductCategory>().Query();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Name.Contains(search));

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductCategoryRowViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();

        return PaginatedResult<ProductCategoryRowViewModel>.Create(items, total, page, pageSize);
    }

    public async Task<ProductCategoryFormViewModel?> GetByIdAsync(Guid id)
    {
        var entity = await uow.Repository<ProductCategory>().GetByIdAsync(id);
        if (entity is null) return null;

        return new ProductCategoryFormViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
        };
    }

    public async Task CreateAsync(ProductCategoryFormViewModel vm)
    {
        var entity = new ProductCategory
        {
            Name = vm.Name,
            Description = vm.Description,
            IsActive = vm.IsActive,
        };

        await uow.Repository<ProductCategory>().AddAsync(entity);
        await uow.CommitAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, ProductCategoryFormViewModel vm)
    {
        var entity = await uow.Repository<ProductCategory>().GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = vm.Name;
        entity.Description = vm.Description;
        entity.IsActive = vm.IsActive;

        uow.Repository<ProductCategory>().Update(entity);
        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await uow.Repository<ProductCategory>().GetByIdAsync(id);
        if (entity is null) return false;

        uow.Repository<ProductCategory>().SoftDelete(entity, currentUser.UserId ?? "system");
        await uow.CommitAsync();
        return true;
    }
}
