using System;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Events.Get
{
    public class EventGyById
    {
        public static string Template => "/eventos/{id:int}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(int id, ApplicationDbContext context)
        {
            var response = context.Eventos.FirstOrDefault(e => e.Id == id);

            if (response == null)
                return Results.NotFound();

            return Results.Ok(response);
        }
    }
}

