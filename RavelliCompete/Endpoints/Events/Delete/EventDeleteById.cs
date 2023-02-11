using System;
using Microsoft.AspNetCore.Mvc;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Events.Delete;

public class EventDeleteById
{
    public static string Template => "/eventos/{id}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action([FromRoute] int id, ApplicationDbContext context) {
        var eventInstance = context.Eventos.FirstOrDefault(c => c.Id == id);

        if (eventInstance == null)
            return Results.NotFound();

        context.Eventos.Remove(eventInstance);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}

