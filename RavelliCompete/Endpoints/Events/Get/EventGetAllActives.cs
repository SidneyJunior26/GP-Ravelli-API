using System;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;

namespace RavelliCompete.Endpoints.Events.Get;

public class EventGetAllActives
{
    public static string Template => "/eventos/{active:bool}/{subscription:bool}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(bool active, bool subscription, ApplicationDbContext context)
    {
        var response = context.Event.Where(e => e.AtivaEvento == active &&
                                                  e.AtivaInscricao == subscription &&
                                                  e.DataFimInscricao >= DateTime.Now &&
                                                  e.DataIniInscricao <= DateTime.Now)
                                                  .ToList();

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

