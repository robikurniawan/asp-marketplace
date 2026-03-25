using AspMarketplace.Web.Controllers;
using AspMarketplace.Web.Interfaces;
using AspMarketplace.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspMarketplace.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProductCategoryController(
    IProductCategoryService service,
    ICurrentUserService currentUser) : BaseController(currentUser)
{
    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        var result = await service.GetPagedAsync(search, page);

        var vm = new ProductCategoryIndexViewModel
        {
            Items = result.Items,
            Search = search,
            Page = result.Page,
            TotalPages = result.TotalPages,
        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create() => View(new ProductCategoryFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCategoryFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        await service.CreateAsync(vm);
        SetSuccess("Kategori berhasil ditambahkan.");
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var vm = await service.GetByIdAsync(id);
        if (vm is null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductCategoryFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var updated = await service.UpdateAsync(id, vm);
        if (!updated) return NotFound();

        SetSuccess("Kategori berhasil diperbarui.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted) return NotFound();

        SetSuccess("Kategori berhasil dihapus.");
        return RedirectToAction(nameof(Index));
    }
}
