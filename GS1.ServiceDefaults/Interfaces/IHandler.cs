using GS1.ServiceDefaults.Models;

namespace GS1.ServiceDefaults.Interfaces;

/*
 * Interfaces voor handlers. 
 * Er bestaat een interface voor void handlers als handlers die een waarde terug geven.
*/
public interface IHandler<TRequest>
{
    Task<Result> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IHandler<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}
