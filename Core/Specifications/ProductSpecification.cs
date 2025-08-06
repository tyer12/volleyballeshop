using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParames specParames) : base(p =>
        (string.IsNullOrEmpty(specParames.Search) || p.Name.ToLower().Contains(specParames.Search)) &&
        (specParames.Brands.Count == 0 || specParames.Brands.Contains(p.Brand)) &&
        (specParames.Types.Count == 0 || specParames.Types.Contains(p.Type))
        )
        {
        ApplyPaging(specParames.PageSize * (specParames.PageIndex - 1), specParames.PageSize);
        switch (specParames.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }    
    }
}
