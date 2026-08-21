namespace GS1.ServiceDefaults.Interfaces;

/* 
 * Marker interfaces die gebruikt worden in de IHandler.
 * IBaseRequest bestaat voor eenvoudige registratie van alle IHandler implementaties.
*/
public interface IRequest : IBaseRequest { }

public interface IRequest<out TResponse> : IBaseRequest { }

public interface IBaseRequest { }
