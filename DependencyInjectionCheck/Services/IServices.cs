namespace DependencyInjectionCheck.Services;

public interface IServices
{
    Guid GetOperationId();
}

public class Service : IServices
{
    private readonly Guid _operationId;

    public Service()
    {
        _operationId = Guid.NewGuid();
    }

    public Guid GetOperationId() => _operationId;
}
