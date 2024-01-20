using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using SporcialAPI.Entities;
using SporcialAPI.Services;
using System.Runtime.CompilerServices;

namespace SporcialAPI.Endpoints;

public static class ActivitiesEndpoints
{
    public static void RegisterActivitiesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/activities", async (Activity activity, IActivityService _activityService) =>
        {
            await _activityService.AddActivityAsync(activity);
            return Results.Created($"{activity.Id}", activity);
        })
        .WithName("AddLivro")
        .RequireAuthorization()
        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
           Summary = "Add new activity, async.",
           Description = "Used for adding new activities.",
           Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Sporcial Activity" } }
        });


        endpoints.MapGet("/activities", async (IActivityService _activityService) =>
                   TypedResults.Ok(await _activityService.GetActivitiesAsync()))
                   .WithName("GetActivities")
                   .WithOpenApi(x => new OpenApiOperation(x)
                   {
                       Summary = "Returns all activities, async.",
                       Description = "Used for returning all activities.",
                       Tags = new List<OpenApiTag> { new() { Name = "Sporcial Activity" } }
                   });

        endpoints.MapGet("/activities/{id}", async (IActivityService _activityService, string id) =>
        {
            var activity = await _activityService.GetActivityAsync(Guid.Parse(id));

            if (activity != null)
                return Results.Ok(activity);
            else
                return Results.NotFound();
        })
            .WithName("GetActivityWithId")
            .RequireAuthorization()
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Returns activity by id, async.",
                Description = "Returns specified activity.",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Sporcial Activity" } }
            });

        endpoints.MapDelete("/activities/{id}", async (string id, IActivityService _activityService) =>
        {
            await _activityService.DeleteActivityAsync(Guid.Parse(id));
            return Results.Ok($"Activity id={id} deleted");
        })
        .WithName("DeleteActivityWithId")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Deletes activity by id, async.",
            Description = "Deletes activity.",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Sporcial Activity" } }
        });

        endpoints.MapPut("/activities/{id}", async (string id, Activity activity, IActivityService _activityService) =>
        {
            if (activity is null)
                return Results.BadRequest("Dados inválidos");

            if (id != activity.Id.ToString())
                return Results.BadRequest();

            await _activityService.UpdateActivityAsync(activity);

            return Results.Ok(activity);
        })
        .WithName("UpdateActivityWithId")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Updates activity by id, async.",
            Description = "Updates activity.",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Sporcial Activity" } }
        });
    }
}
