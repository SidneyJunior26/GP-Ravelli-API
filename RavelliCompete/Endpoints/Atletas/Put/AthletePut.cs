using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Infra.Data;
using System.Security.Claims;
using RavelliCompete.Domain.Athletes;

namespace RavelliCompete.Endpoints.Atletas.Put;

public class AthletePut
{
    public static string Template => "/athletes/{cpf}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    [Authorize]
    public static async Task<IResult> Action(
        string Cpf, AthleteRequest athleteRequest, ApplicationDbContext context, HttpContext http)
    {
        var athlete = context.Athlete.FirstOrDefault(c => c.Cpf == Cpf);

        if (athlete == null)
            return Results.NotFound();

        athlete.EditAthlete(athleteRequest);

        if (!athlete.IsValid)
            return Results.ValidationProblem(athlete.Notifications.ConvertToProblemDetails());

        await context.SaveChangesAsync();

        return Results.Ok(athlete);
    }
}

