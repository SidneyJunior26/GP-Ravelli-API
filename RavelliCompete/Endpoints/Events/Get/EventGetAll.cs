using System;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;

namespace RavelliCompete.Endpoints.Events.Get;

public class EventGetAll
{
    public static string Template => "/eventos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context) {
        var response = context.Eventos.OrderByDescending(e => e.Data).ToList();

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

