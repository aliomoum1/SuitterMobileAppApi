namespace A2Z.WebApi.Application.Folder.Names;

public class NameByNameSpec : Specification<Name>, ISingleResultSpecification
{
    public NameByNameSpec(string name) => Query.Where(p => p.Name == name);
       
}