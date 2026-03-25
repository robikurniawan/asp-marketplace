namespace AspMarketplace.Web.ViewModels.Admin;

public class ProductCategoryIndexViewModel
{
    public IEnumerable<ProductCategoryRowViewModel> Items { get; set; } = [];
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int TotalPages { get; set; }
}

public class ProductCategoryRowViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductCategoryFormViewModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}
