using System;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.Get;

public class AthletesGetById
{
    public static string Template => "/atletas/id/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(string id, ApplicationDbContext context)
    {
        var response = context.Athlete.FirstOrDefault(a => a.Id == id);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

