using GS1.ServiceDefaults.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace GS1.ApiService.Features.Items.UpdateItem;

public static class UpdateItemEndpoint
{
    /*
     * Endpoint voor het wijzigen van een item.
     * Authenticatie is nog niet geïmplementeerd en endpoint geeft dus nog geen '401' terug.
     * Hier wordt (nog) niet gevalideerd of het een valide GTIN is. Als die niet valide is krijgt de gebruiker namelijk een 404 terug.
    */
    internal static void MapUpdateItemEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPut("{gtin}", Create)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict);
    }

    private static async Task<IResult> Create([FromRoute] string gtin, [FromBody] UpdateItemRequest request, UpdateItemHandler handler, CancellationToken cancellationToken)
    {
        request.Gtin = gtin;

        var result = await handler.Handle(request, cancellationToken);

        return result.ToTypedResult();
    }
}
