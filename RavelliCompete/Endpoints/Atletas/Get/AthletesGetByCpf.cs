using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.Get;

public class AthletesGetByCpf
{
    public static string Template => "/athletes/{cpf}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize]
    public static async Task<IResult> Action(string cpf, ApplicationDbContext context)
    {
        var response = await context.Athlete.FirstOrDefaultAsync(a => a.Cpf == cpf);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}