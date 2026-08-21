using GS1.ApiService.Features.Items.CreateItem;
using GS1.ApiService.Features.Items.GetItem;
using GS1.ApiService.Features.Items.UpdateItem;

namespace GS1.ApiService.Features.Items;

public static class ItemsEndpoints
{
    public static WebApplication MapItemsEndpoints(this WebApplication webApplication)
    {
        var group = webApplication
            .MapGroup("api/items");

        group.MapCreateItemEndpoint();
        group.MapGetItemEndpoint();
        group.MapUpdateItemEndpoint();

        return webApplication;
    }
}
