using System;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;

namespace RavelliCompete.Endpoints.Events.Get;

public class EventGetAllComing {
    public static string Template => "/eventos/coming";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context) {
        var response = context.Event.Where(e => e.DataIniInscricao > DateTime.Now)
                                                  .OrderByDescending(e => e.DataIniInscricao)
                                                  .ToList();

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

