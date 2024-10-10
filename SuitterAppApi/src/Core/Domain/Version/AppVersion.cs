namespace SuitterAppApi.Domain.Version;
public class AppVersion : AuditableEntity, IAggregateRoot
{
    public AppVersion(string number, string note)
    {
        Number = number;
        Note = note;
    }
    public AppVersion Update(string number, string note)
    {
        Number = number;
        Note = note;
        return this;
    }
    public string Number { get; private set; }
    public string Note { get; private set; }
}
