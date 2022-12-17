using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.Delete;

public class AthleteDelete
{
    public static string Template => "/athletes/{id}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action([FromRoute] string id, ApplicationDbContext context)
    {
        var athlete = context.Athlete.FirstOrDefault(c => c.Id == id);

        if (athlete == null)
            return Results.NotFound();

        context.Athlete.Remove(athlete);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}

