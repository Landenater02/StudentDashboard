using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentDashboard.Data;
using StudentDashboard.Models;
namespace StudentDashboard.Controllers;

public static class VisitsEndpoints
{
    public static void MapVisitsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Visits").WithTags(nameof(Visits));

        group.MapGet("/", async (StudentDashboardContext db) =>
        {
            return await db.Visits.ToListAsync();
        })
        .WithName("GetAllVisits")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Visits>, NotFound>> (int id, StudentDashboardContext db) =>
        {
            return await db.Visits.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Visits model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetVisitsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Visits visits, StudentDashboardContext db) =>
        {
            var affected = await db.Visits
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, visits.Id)
                    .SetProperty(m => m.Advisor, visits.Advisor)
                    .SetProperty(m => m.Student, visits.Student)
                    .SetProperty(m => m.Description, visits.Description)
                    .SetProperty(m => m.Date, visits.Date)
                    .SetProperty(m => m.File, visits.File)
                    .SetProperty(m => m.ParentsCalled, visits.ParentsCalled)
                    .SetProperty(m => m.length, visits.length)
                    .SetProperty(m => m.Topics, visits.Topics)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateVisits")
        .WithOpenApi();

        group.MapPost("/", async (Visits visits, StudentDashboardContext db) =>
        {
            db.Visits.Add(visits);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Visits/{visits.Id}",visits);
        })
        .WithName("CreateVisits")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentDashboardContext db) =>
        {
            var affected = await db.Visits
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteVisits")
        .WithOpenApi();
    }
}
