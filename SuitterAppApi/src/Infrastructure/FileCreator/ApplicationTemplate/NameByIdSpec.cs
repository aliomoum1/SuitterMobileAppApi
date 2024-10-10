namespace SuitterAppApi.Application.Folder.Change22s;

public class Change22ByIdSpec : Specification<Change22, Change22DetailsDto>, ISingleResultSpecification
{
    public Change22ByIdSpec(Guid id) =>Query.Where(p => p.Id == id);
        
           
            
}