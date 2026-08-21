using GS1.ServiceDefaults.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace GS1.ServiceDefaults.Models.Extensions;

/*
 * Extension methods om van een Result object naar een TypedResult te gaan.
 * Dit zorgt ervoor dat er in het MinimalApi endpoint alleen maar een simpele '.ToTypedResult' hoeft aangeroepen te worden.
*/
public static class ResultExtensions
{
    public static IResult ToTypedResult(this Result result, Uri? createdUri = null)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.Type == ResultType.Success && createdUri != null)
        {
            return TypedResults.Created(createdUri);
        }

        return result.Type switch
        {
            ResultType.Success => TypedResults.NoContent(),
            ResultType.ValidationFailed => TypedResults.ValidationProblem(result.ValidationErrors!),
            ResultType.NotFound => TypedResults.Problem(statusCode: StatusCodes.Status404NotFound, detail: result.ErrorMessage),
            ResultType.Conflict => TypedResults.Problem(statusCode: StatusCodes.Status409Conflict, detail: result.ErrorMessage),
            ResultType.Forbidden => TypedResults.Problem(statusCode: StatusCodes.Status403Forbidden, detail: result.ErrorMessage),
            ResultType.Failure => TypedResults.Problem(statusCode: StatusCodes.Status500InternalServerError, detail: result.ErrorMessage),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    }

    public static IResult ToTypedResult<T>(this Result<T> result, Uri? createdUri = null)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.Type == ResultType.Success && createdUri != null)
        {
            return TypedResults.Created(createdUri, result.Value);
        }

        return result.Type switch
        {
            ResultType.Success => TypedResults.Ok(result.Value),
            ResultType.ValidationFailed => TypedResults.ValidationProblem(result.ValidationErrors!),
            ResultType.NotFound => TypedResults.Problem(statusCode: StatusCodes.Status404NotFound, detail: result.ErrorMessage),
            ResultType.Conflict => TypedResults.Problem(statusCode: StatusCodes.Status409Conflict, detail: result.ErrorMessage),
            ResultType.Forbidden => TypedResults.Problem(statusCode: StatusCodes.Status403Forbidden, detail: result.ErrorMessage),
            ResultType.Failure => TypedResults.Problem(statusCode: StatusCodes.Status500InternalServerError, detail: result.ErrorMessage),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    }
}
