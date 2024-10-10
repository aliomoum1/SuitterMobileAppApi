namespace A2Z.WebApi.Application.Catalog.Names;

public class NamesByBrandSpec : Specification<Name>
{
    public NamesByBrandSpec(Guid brandId) =>Query.Where(p => p.BrandId == brandId);
        
}
