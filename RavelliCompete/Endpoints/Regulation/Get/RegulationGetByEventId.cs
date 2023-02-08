using System;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Regulation.Get
{
    public class RegulationGetByEventId
    {
        public static string Template => "/regulation/{eventId:int}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handler => Action;

        public static IResult Action(int eventId, ApplicationDbContext context) {
            var response = context.Regulamentos.FirstOrDefault(e => e.IdEvento == eventId);

            if (response == null)
                return Results.NotFound();

            return Results.Ok(response);
        }
    }
}

