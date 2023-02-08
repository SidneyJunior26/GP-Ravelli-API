using System;
using Microsoft.AspNetCore.Authorization;
using RavelliCompete.Infra.Data;
using RavelliCompete.Domain.Athletes;

namespace RavelliCompete.Endpoints.MedicalRecord.Get;

public class MedicalRecordGetByAthleteId
{
    public static string Template => "/medical/{idAthlete}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize]
    public static IResult Action(string idAthlete, ApplicationDbContext context) {
        var response = context.MedicalAthlete.FirstOrDefault(m => m.IdAtleta == idAthlete);

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

