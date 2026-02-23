namespace WarehouseMonitor.Domain.Common;

public abstract class BaseEntity
{

    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IReadOnlyList<DomainEvent> PullDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();
        _domainEvents.Clear();
        return domainEvents;
    }
}