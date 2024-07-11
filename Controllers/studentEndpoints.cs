using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using StudentDashboard.Data;
using StudentDashboard.Models;
namespace StudentDashboard.Controllers;

public static class studentEndpoints
{
    public static void MapstudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/student").WithTags(nameof(student));

        group.MapGet("/", async (StudentDashboardContext db) =>
        {
            return await db.student.ToListAsync();
        })
        .WithName("GetAllstudents")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<student>, NotFound>> (int id, StudentDashboardContext db) =>
        {
            return await db.student.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetstudentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, student student, StudentDashboardContext db) =>
        {
            var affected = await db.student
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, student.Id)
                    .SetProperty(m => m.Name, student.Name)
                    .SetProperty(m => m.Advisor, student.Advisor)
                    .SetProperty(m => m.Grade, student.Grade)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("Updatestudent")
        .WithOpenApi();

        group.MapPost("/", async (student student, StudentDashboardContext db) =>
        {
            db.student.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/student/{student.Id}",student);
        })
        .WithName("Createstudent")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, StudentDashboardContext db) =>
        {
            var affected = await db.student
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("Deletestudent")
        .WithOpenApi();
    }
}
