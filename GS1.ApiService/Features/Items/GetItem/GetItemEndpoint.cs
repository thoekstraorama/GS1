using GS1.ServiceDefaults.Models.Extensions;

namespace GS1.ApiService.Features.Items.GetItem;

public static class GetItemEndpoint
{
    /*
     * Endpoint voor het ophalen van een item.
     * Dit endpoint bevat geen authenticatie/autorisatie. Hiervoor heb ik de aanname gedaan dat de informatie die teruggeven wordt publiek is.
    */
    internal static void MapGetItemEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("{gtin}", Get)
            .Produces<GetItemResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> Get([AsParameters] GetItemRequest request, GetItemHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToTypedResult();
    }
}
