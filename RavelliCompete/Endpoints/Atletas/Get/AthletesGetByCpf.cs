using System;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.Get;

public class AthletesGetByCpf
{
    public static string Template => "/atletas/{cpf}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(string cpf, ApplicationDbContext context)
    {
        var response = context.Athlete.FirstOrDefault(a => a.Cpf == cpf);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

