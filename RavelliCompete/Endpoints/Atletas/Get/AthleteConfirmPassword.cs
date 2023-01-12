using System;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Endpoints.Atletas;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.GetConfirmPassword;

public class AthleteConfirmPassword
{
    public static string Template => "/atletas/confirmar/{cpf}/{password}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(string cpf, string password, ApplicationDbContext context) {
        var userOk = context.Athlete.FirstOrDefault(a => a.Cpf == cpf && a.Acesso == password);

        if (userOk == null)
            return Results.NotFound();

        return Results.Ok();
    }
}