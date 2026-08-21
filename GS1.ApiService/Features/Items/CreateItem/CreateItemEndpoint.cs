using Microsoft.AspNetCore.Mvc;
using GS1.ServiceDefaults.Models.Extensions;

namespace GS1.ApiService.Features.Items.CreateItem;

public static class CreateItemEndpoint
{
    /*
     * Endpoint om een Item aan te maken.
     * Authenticatie is nog niet geïmplementeerd en endpoint geeft dus nog geen '401' terug.
    */
    internal static void MapCreateItemEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(string.Empty, Create)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status409Conflict);
    }

    private static async Task<IResult> Create([FromBody] CreateItemRequest request, CreateItemHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToTypedResult(new Uri($"api/items/{result.Value?.Gtin}", UriKind.Relative));
    }
}
