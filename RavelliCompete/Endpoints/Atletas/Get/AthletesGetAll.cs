using System;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;

namespace RavelliCompete.Endpoints.Atletas.GetAll;

public class AthletesGetAll
{
    public static string Template => "/athletes";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(int? page, int? rows, QueryAllAthletesWithPagination query)
    {
        if (page == null)
            page = 1;

        if (rows == null)
            rows = 10;

        var response = await query.Execute(page.Value, rows.Value);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

